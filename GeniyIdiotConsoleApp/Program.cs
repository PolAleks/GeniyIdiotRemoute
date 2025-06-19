namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int countQuestions = 5;

            (string[] questions, string[] answers) = GetQuestionsAndAnswer();

            int countCorrectAnswer = 0;
            
            for (int i = 0; i < questions.Length; i++)
            {
                Console.WriteLine(questions[i]);
                (string userAnswer, string correctAnswer) = (Console.ReadLine(), answers[i]);
                if (userAnswer == correctAnswer)
                    countCorrectAnswer++;
            }

            Console.WriteLine(countCorrectAnswer);
        }
        static (string[], string[]) GetQuestionsAndAnswer()
        {
            string[] questions =
            [
                "Сколько будет два плюс два умноженное на два?",
                "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?",
                "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
                "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?",
                "Пять свечей горело, две потухли. Сколько свечей осталось?"
            ];

            string[] answers = ["6", "9", "25", "60", "2"];

            return (questions, answers);
        }
    }
}
