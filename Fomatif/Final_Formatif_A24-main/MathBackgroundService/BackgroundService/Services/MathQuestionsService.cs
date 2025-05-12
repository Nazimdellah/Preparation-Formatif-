using BackgroundServiceVote.Models;

namespace BackgroundServiceVote.Services
{
    public class MathQuestionsService
    {
        private const int MAX_VALUE = 20;
        private readonly Random _random;

        public MathQuestionsService() {
            _random = new Random((int)DateTime.Now.Ticks);
        }

        public MathQuestion CreateQuestion()
        {
            MathQuestion mathQuestion = new()
            {
                Operation = (Operation)_random.Next(0, 3),
                ValueA = _random.Next(1, MAX_VALUE),
                ValueB = _random.Next(1, MAX_VALUE)
            };

            List<int> answers = new List<int>
            {
                mathQuestion.ValueA + mathQuestion.ValueB,
                mathQuestion.ValueA - mathQuestion.ValueB,
                mathQuestion.ValueA * mathQuestion.ValueB
            };

            int rightAnswer;
            if (mathQuestion.Operation == Operation.Add)
                rightAnswer = mathQuestion.ValueA + mathQuestion.ValueB;
            else if (mathQuestion.Operation == Operation.Substract)
                rightAnswer = mathQuestion.ValueA - mathQuestion.ValueB;
            else
                rightAnswer = mathQuestion.ValueA * mathQuestion.ValueB;

            mathQuestion.Answers = answers.OrderBy(x => _random.Next()).ToList();
            mathQuestion.RightAnswerIndex = mathQuestion.Answers.IndexOf(rightAnswer);

            return mathQuestion;
        }

    }
}
