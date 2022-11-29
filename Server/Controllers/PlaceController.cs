using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _context.Places.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Route("PostPlace"), HttpPost]
        public async Task<ActionResult> Post([FromBody] PlaceDto Place)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Places.Add(new Place { Description = Place.Description });
                    await _context.SaveChangesAsync();

                    return Ok(new { Message = "Um novo de local para Universidade foi criado." });
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

        [Route("UpdatePlace"), HttpPut]
        public async Task<ActionResult> Put([FromBody] PlaceDto placeDescription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Place = await _context.Places.FindAsync(placeDescription.Id);

                    if (Place == null)
                        return NotFound(new { Message = "Local não encontrado." });

                    Place.Description = placeDescription.Description;

                    _context.Places.Update(Place);
                    await _context.SaveChangesAsync();

                    return Ok(new { Message = "Local atualizado com sucesso" });
                }
                else
                {
                    return BadRequest(new { Message = "Campos do Body inválidos." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Route("DeletePlace/{id}"), HttpDelete]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("O Id na URL é inválido.");

                var Place = await _context.Places.FindAsync(id);

                if (Place == null)
                    return NotFound(new { Message = "Local não encontrado." });

                _context.Places.Remove(Place);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Local removido com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}