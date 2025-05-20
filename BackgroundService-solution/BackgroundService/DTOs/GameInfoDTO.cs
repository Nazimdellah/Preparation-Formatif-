using System.ComponentModel.DataAnnotations;

namespace BackgroundService.DTOs
{
    public class GameInfoDTO
    {
        public int MultiplierCost { get; set; }
        public int NbWins { get; set; }
    }
}
