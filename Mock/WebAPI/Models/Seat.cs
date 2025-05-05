using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public virtual string? ExamenUserId { get; set; }
        public virtual ExamenUser? ExamenUser { get; set; }
    }
}
