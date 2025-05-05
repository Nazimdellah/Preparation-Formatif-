using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ExamenUser:IdentityUser
    {
        public Seat? Seat { get; set; }
    }
}
