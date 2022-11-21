using Microsoft.AspNetCore.Mvc;
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

        public AuthController(WheresthekeyContext context)
        {
            _context = context;
        }

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

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("CriarMae"), HttpPost]
        public async Task<ActionResult> MaeDeTodos()
        {
            try
            {
                CryptographyService.CreatePasswordHash("abc123", out byte[] passwordHash, out byte[] passwordSalt);

                _context.People.Add(new Person
                {
                    Id = "cv1000000",
                    Name = "Arlete",
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    AccountStatusId = (int)EPersonStatus.Approved,
                    RolePersonId = (int)ERole.Administrator
                });
                await _context.SaveChangesAsync();

                return Ok("Arlete Criada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("TestListPeople"), HttpGet]
        public ActionResult TestGetPeople()
        {
            try
            {
                return Ok(_context.People.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
