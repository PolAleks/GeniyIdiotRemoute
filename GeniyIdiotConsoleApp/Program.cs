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
                Console.WriteLine($"Вопрос №{i + 1} \n{questions[i]}");
                (string userAnswer, string correctAnswer) = (Console.ReadLine(), answers[i]);
                if (userAnswer == correctAnswer)
                    countCorrectAnswer++;
            }

            string diagnose = countCorrectAnswer switch
            {
                0 => "Идиот",
                1 => "Кретин",
                2 => "Дурак",
                3 => "Нормальный",
                4 => "Талант",
                5 => "Гений"
            };

            Console.WriteLine($"Твой диагноз - Ты {diagnose}");
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
