namespace GeniyIdiotConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            do
            {
                (string[] questions, int[] answers) = GetQuestionsAndAnswer();

                int countCorrectAnswer = 0;

                Console.Write("Напиши своё имя: ");
                string userName = Console.ReadLine() ?? "Неизвестный";

                for (int i = 0; i < questions.Length; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1} \n{questions[i]}");
                    (int userAnswer, int correctAnswer) = (GetUserAnswer(), answers[i]);
                    if (userAnswer == correctAnswer)
                        countCorrectAnswer++;
                }

                string diagnose = GetDiagnoses(countCorrectAnswer);

                Console.WriteLine($"{userName}, твой диагноз - {diagnose}");
            }
            while (RepeatAgain());
        }

        static int GetUserAnswer()
        {
            while(true)
            {
                if(int.TryParse(Console.ReadLine(), out int answer))
                    return answer;
                Console.Write("Пожалуйста, введите число! ");
            }  
        }
        static bool RepeatAgain()
        {
            Console.Write("Есть желание повторить тест? (да/нет): ");
            while(true)
            {
                string input = Console.ReadLine()?.ToLower() ?? string.Empty;
                if (input == "да") return true;
                else if (input == "нет") return false;
                else Console.Write("Некорректный ввод! Повторить тест? (да/нет): ");
            }
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

        static (string[], int[]) GetQuestionsAndAnswer()
        {
            string[] questions =
            [
                "Сколько будет два плюс два умноженное на два?",
                "Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?",
                "На двух руках 10 пальцев. Сколько пальцев на 5 руках?",
                "Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?",
                "Пять свечей горело, две потухли. Сколько свечей осталось?"
            ];

            int[] answers = [ 6, 9, 25, 60, 2 ];

            Shuffles(questions, answers);

            return (questions, answers);
        }

        static void Shuffles(string[] questions, int[] answers)
        {
            Random random = new Random();
            for (int currentIndex = questions.Length - 1; currentIndex > 0; currentIndex--)
            {
                int newIndex = random.Next(currentIndex);
                (questions[currentIndex], questions[newIndex]) = (questions[newIndex], questions[currentIndex]);
                (answers[currentIndex], answers[newIndex]) = (answers[newIndex], answers[currentIndex]);
            }
        }
    }
}
