using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Server.Models;
using Server.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        #region Methods for everyone
        [Route("ListMyReservations"), HttpGet]
        public async Task<ActionResult> ListMyReservations()
        {
            try
            {
                string userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

                if (String.IsNullOrEmpty(userId))
                    return Unauthorized(new { Message = "Ocorreu um erro na sua autenticação. Por favor, faça o login novamente. Se o erro persistir, contate o suporte." });

                var reservations = await _context.Reservations.Where(r => r.PersonId == userId).ToListAsync();

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Route("CreateNewReservation"), HttpPost]
        public async Task<ActionResult> CreateNewReservation([FromBody] ReservationDto reservationDto)
        {
            try
            {
                if (ModelState.IsValid && reservationDto.PlaceId > 0)
                {
                    string userId = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                    DateTime startDate = Convert.ToDateTime(reservationDto.StartDate);
                    DateTime endDate = Convert.ToDateTime(reservationDto.EndDate);
                    Reservation reservation = new Reservation();

                    if (String.IsNullOrEmpty(userId))
                        return Unauthorized("Ocorreu um erro na sua autenticação. Por favor, faça o login novamente. Se o erro persistir, contate o suporte.");

                    // Check if there is a reservation where the date range overlaps the chosen range
                    CustomModel placeIsAvailable = await PlaceIsAvailable(reservationDto.PlaceId, startDate, endDate);

                    if (!placeIsAvailable.Result)
                        throw new Exception(placeIsAvailable.Message);

                    reservation = new Reservation
                    {
                        PlaceId = reservationDto.PlaceId,
                        PersonId = userId,
                        StartDate = startDate,
                        EndDate = endDate,
                        ReservationStatus = (int)EReservationStatus.Pending
                    };

                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    return Ok(new
                    {
                        Message = "Sua reserva foi registrada com sucesso. O Administrador irá analisa-la para saber se está de acordo."
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
        #endregion

        #region Methods for Admins
        [Route("ListReservations"), HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ListReservations()
        {
            try
            {
                var reservations = await _context.Reservations.ToListAsync();

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Route("UpdateReservation"), HttpPut]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> UpdateReservation([FromBody] ReservationChangeStatusDto reservationToChange)
        {
            try
            {
                var reservation = await _context.Reservations.Where(r => r.Id == reservationToChange.Id).FirstOrDefaultAsync();

                if (reservation == null)
                    return NotFound(new { Message = "Reserva não encontrada." });

                CustomModel placeIsAvailable = await PlaceIsAvailable(reservation.PlaceId, reservation.StartDate, reservation.EndDate);

                if (!placeIsAvailable.Result)
                    throw new Exception(placeIsAvailable.Message);

                reservation.ReservationStatus = reservationToChange.ReservationStatusId;

                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Reserva atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [Route("DeleteReservation/{reservationId}"), HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DeleteReservation([FromRoute] int reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.Where(r => r.Id == reservationId).FirstOrDefaultAsync();

                if (reservation == null)
                    return NotFound(new { Message = "Reserva não encontrada." });

                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Reserva excluída com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        #endregion

        private async Task<CustomModel> PlaceIsAvailable(int placeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var reservation = await _context.Reservations
                            .Include(r => r.Place)
                            .Where
                            (
                                r => r.PlaceId == placeId
                                && r.ReservationStatus == (int)EReservationStatus.Approved
                                && (startDate <= r.EndDate && endDate >= r.StartDate)
                            ).FirstOrDefaultAsync();

                if (reservation != null)
                {
                    return new CustomModel
                    {
                        Result = false,
                        Message = $"Já existe uma reserva feita para o(a) {reservation.Place.Description} na data: {reservation.StartDate.ToString()} até {reservation.EndDate.ToString()}"
                    };
                }
                else
                {
                    return new CustomModel
                    {
                        Result = true,
                        Message = ""
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro inesperado em PlaceIsAvailable. Por favor, contate o suporte.");
            }
        }
    }
}