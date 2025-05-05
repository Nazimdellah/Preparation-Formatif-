using WebAPI.Data;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class SeatsService
    {
        private readonly WebAPIContext _context;

        private const int MAX_SEATS = 100;

        public SeatsService()
        {

        }

        public SeatsService(WebAPIContext context)
        {
            _context = context;
        }


        public virtual Seat ReserveSeat(string userid, int seatnumber)
        {
            if (seatnumber > MAX_SEATS)
            {
                throw new SeatOutOfBoundsException();
            }

            var user = _context.Users.Single(u => u.Id == userid);
            if (user.Seat != null)
            {
                throw new UserAlreadySeatedException();
            }

            var seat = _context.Seats.SingleOrDefault(s => s.Number == seatnumber);
            if(seat == null)
            {
                seat = new Seat() { Number = seatnumber };
            }

            if(seat.ExamenUser != null)
            {
                throw new SeatAlreadyTakenException();
            }

            seat.ExamenUserId = user.Id;
            seat.ExamenUser = user;
            _context.SaveChanges();


            return seat;
        }
    }
}

