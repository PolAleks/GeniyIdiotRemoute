namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                (string[] questions, string[] answers) = GetQuestionsAndAnswer();

                int countCorrectAnswer = 0;

                Console.Write("Напиши своё имя: ");
                string userName = Console.ReadLine() ?? "Неизвестный";

                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1} \n{questions[i]}");
                    (string userAnswer, string correctAnswer) = (Console.ReadLine() ?? "0", answers[i]);
                    if (userAnswer == correctAnswer)
                        countCorrectAnswer++;
                }

                string diagnose = GetDiagnoses(countCorrectAnswer);

                Console.WriteLine($"{userName}, твой диагноз - {diagnose}");
            }
            while (RepeatAgain());
        }

        static bool RepeatAgain()
        {
            Console.Write("Есть желание повторить тест? (да/нет): ");
            return Console.ReadLine()?.ToLower() == "да";
        }

        static string GetDiagnoses(int countCorrectAnswer) => countCorrectAnswer switch
        {
            0 => "Идиот",
            1 => "Кретин",
            2 => "Дурак",
            3 => "Нормальный",
            4 => "Талант",
            5 => "Гений"
        };

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

            Random random = new Random();
            for (int currentIndex = 0; currentIndex < questions.Length; currentIndex++)
            {
                int newIndex = random.Next(questions.Length);
                (questions[currentIndex], questions[newIndex]) = (questions[newIndex], questions[currentIndex]);
                (answers[currentIndex], answers[newIndex]) = (answers[newIndex], answers[currentIndex]);
            }

            return (questions, answers);
        }
    }
}
