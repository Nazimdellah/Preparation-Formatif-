using System.Text.Json.Serialization;

namespace BackgroundServiceVote.Models
{
    public enum Operation
    {
        Add,
        Substract,
        Multiply
    }

    public class MathQuestion
    {
        public Operation Operation { get; set; }
        public int ValueA { get; set; }
        public int ValueB { get; set; }
        public List<int> Answers { get; set; }
        [JsonIgnore]
        public int RightAnswerIndex { get; set; }
        public int[] PlayerChoices { get; set; } = new int[3];
    }
}
