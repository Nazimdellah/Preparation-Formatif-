using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatsController : ControllerBase
    {

        public virtual string? UserId
        {
            get
            {
                return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            }
        }

        public SeatsService _service;

        public SeatsController(SeatsService service)
        {
            _service = service;
        }

        // POST: api/Seats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("{seatnumber}")]
        public ActionResult<Seat> ReserveSeat(int seatnumber)
        {
            try
            {
                var seat = _service.ReserveSeat(UserId!, seatnumber);
                return Ok(seat);
            }
            catch (SeatAlreadyTakenException)
            {
                return Unauthorized();
            }
            catch (SeatOutOfBoundsException)
            {
                return NotFound("Could not find " + seatnumber);
            }
            catch (UserAlreadySeatedException)
            {
                return BadRequest();
            }
        }

        
    }
}
