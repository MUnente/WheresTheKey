using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Server.Context;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class PlaceTypeController : ControllerBase
    {
        private readonly WheresthekeyContext _context;

        public PlaceTypeController(WheresthekeyContext context)
        {
            _context = context;
        }

        [Route("GetPlaceTypes"), HttpGet]
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

        [Route("PostPlaceType"), HttpPost]
        public async Task<ActionResult> Post([FromBody] PlaceTypeDto placeType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.PlaceTypes.Add(new PlaceType { Description = placeType.Description });
                    await _context.SaveChangesAsync();

                    return Ok("Um tipo novo de local para Universidade foi Criado.");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdatePlaceType/{id}"), HttpPut]
        public async Task<ActionResult> Put([FromBody] PlaceTypeDto placeTypeDescription, [FromRoute] int id)
        {
            try
            {
                if (ModelState.IsValid && id > 0)
                {
                    var placeType = await _context.PlaceTypes.FindAsync(id);

                    if (placeType == null)
                        return NotFound("Tipo de local não encontrado.");

                    placeType.Description = placeTypeDescription.Description;

                    _context.PlaceTypes.Update(placeType);
                    await _context.SaveChangesAsync();

                    return Ok("Local atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Campos do Body ou Id da URL inválidos.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeletePlaceType/{id}"), HttpDelete]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("O Id na URL é inválido.");

                var placeType = await _context.PlaceTypes.FindAsync(id);

                if (placeType == null)
                    return NotFound("Tipo de local não encontrado.");

                _context.PlaceTypes.Remove(placeType);
                await _context.SaveChangesAsync();

                return Ok("Tipo de local removido com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}