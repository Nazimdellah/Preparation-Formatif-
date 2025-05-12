using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController(SeatsService service) : ControllerBase
    {
        public virtual string? UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // POST: api/Seats/4
        [HttpGet("{seatNumber:int}")]
        public ActionResult<Seat> ReserveSeat(int seatNumber)
        {
            try
            {
                Seat seat = service.ReserveSeat(UserId!, seatNumber);
                return Ok(seat);
            }
            catch (SeatAlreadyTakenException)
            {
                return Unauthorized();
            }
            catch (SeatOutOfBoundsException)
            {
                return NotFound("Could not find " + seatNumber);
            }
            catch (UserAlreadySeatedException)
            {
                return BadRequest();
            }
        }
    }
}
