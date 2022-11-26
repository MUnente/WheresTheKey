using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Context;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly WheresthekeyContext _context;

        public ReservationController(WheresthekeyContext context)
        {
            _context = context;
        }

        // [Route("MyReservations"), HttpGet]
        // public Action MyReservations()
        // {

        // }








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