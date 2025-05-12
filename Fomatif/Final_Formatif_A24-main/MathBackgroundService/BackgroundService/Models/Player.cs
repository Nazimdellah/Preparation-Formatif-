using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace BackgroundServiceVote.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [DisplayName("Username")]
        public IdentityUser User { get; set; }
        public int NbRightAnswers { get; set; }
    }
}
