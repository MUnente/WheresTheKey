using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Context;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly WheresthekeyContext _context;

        public ReservationController(WheresthekeyContext context)
        {
            _context = context;
        }

        [Route("GetServidores"), HttpGet]
        [Authorize]
        public ActionResult GetServidores()
        {
            return Ok($"Olá, {User.Identity.Name}! Esta é uma rota aberta para todos.");
        }

        [Route("GetAdmin"), HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult GetAdmin()
        {
            return Ok($"Olá, chefe ({User.Identity.Name})! Esta é uma rota exclusíva apenas para administradores");
        }

        [Route("TestGetPeople"), HttpGet]
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