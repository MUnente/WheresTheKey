using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Context;

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

        [HttpPost]
        public ActionResult Post([FromBody] UserLoginDto user)
        {
            try
            {
                var person = _context.People.Where(p => p.Id == user.Id && p.Password == user.Password).FirstOrDefault();

                if (person == null)
                    throw new Exception("Usuário não existe.");

                return Ok(person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(Name = "GetPeople")]
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
