using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Server.Context;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class PlaceController : ControllerBase
    {
        private readonly WheresthekeyContext _context;

        public PlaceController(WheresthekeyContext context)
        {
            _context = context;
        }

        [Route("GetPlaces"), HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Places.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("PostPlace"), HttpPost]
        public ActionResult Post()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdatePlace"), HttpPut]
        public ActionResult Put()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeletePlace"), HttpDelete]
        public ActionResult Delete()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}