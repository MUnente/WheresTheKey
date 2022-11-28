using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Server.Context;
using Server.Services;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly WheresthekeyContext _context;
        private readonly IConfiguration _config;

        public AuthController(WheresthekeyContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [AllowAnonymous]
        [Route("Login"), HttpPost]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = await _context.People.FindAsync(userLogin.Id);

                    if (person == null)
                        throw new Exception("Servidor não existe.");

                    if (!CryptographyService.VerifyPasswordHash(userLogin.Password, person.Password, person.PasswordSalt))
                        throw new Exception("Senha incorreta.");

                    if (person.AccountStatusId != (int)EAccountStatus.Approved)
                        throw new Exception("Não foi possível permitir seu login. Sua solicitação pode estar pendente, foi reprovada ou sua conta pode ter sido bloqueada. Em caso de dúvidas, contate o Administrador.");

                    var token = new TokenService(_config).GenerateToken(person);

                    return Ok(new { Token = token, Role = person.RolePersonId });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("Register"), HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _context.People.Where(person => person.Id == userRegister.Id).AnyAsync())
                        throw new Exception("Servidor já existente.");

                    CryptographyService.CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    _context.People.Add(new Person
                    {
                        Id = userRegister.Id,
                        Name = userRegister.Name,
                        Password = passwordHash,
                        PasswordSalt = passwordSalt,
                        AccountStatusId = (int)EAccountStatus.Pending,
                        RolePersonId = (int)ERole.Employee
                    });

                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        Message = "Sua solicitação foi registrada com exito. O administrador fará uma análise sobre ela."
                    });
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
