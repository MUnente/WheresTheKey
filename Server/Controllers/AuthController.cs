using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Server.Context;
using Server.Services;

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
        public ActionResult Login([FromBody] UserLoginDto userLogin)
        {
            try
            {
                var person = _context.People.Where(p => p.Id == userLogin.Id).FirstOrDefault();

                if (person == null)
                    throw new Exception("Servidor não existe.");

                if (!CryptographyService.VerifyPasswordHash(userLogin.Password, person.Password, person.PasswordSalt))
                    throw new Exception("Senha incorreta.");

                var token = new TokenService(_config).GenerateToken(person);

                return Ok(new
                {
                    Token = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [Route("Register"), HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto userRegister)
        {
            try
            {
                if (_context.People.Where(person => person.Id == userRegister.Id).Any())
                    throw new Exception("Servidor já existente.");

                CryptographyService.CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

                _context.People.Add(new Person
                {
                    Id = userRegister.Id,
                    Name = userRegister.Name,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    AccountStatusId = (int)EPersonStatus.Pending,
                    RolePersonId = (int)ERole.CivilServant
                });

                await _context.SaveChangesAsync();

                return Ok("Sua solicitação foi registrada com exito. O administrador fará uma análise sobre ela.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
