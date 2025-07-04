namespace GeniyIdiotConsoleApp
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ShowMenu();
        }

        private static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Показать результаты тестирования.");
                Console.WriteLine("2. Пройти тестирование.");
                Console.WriteLine("3. Добавить новый вопрос.");
                Console.Write("Введите номер пункта меню от 1 до 3: ");
                string userChoice = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(userChoice, out int choce))
                {
                    switch (choce)
                    {
                        case 1:
                            ShowResultTesting();
                            break;
                        case 2:
                            StartTesting();
                            break;
                        case 3:
                            QuestionStorage.AddQuestion();
                        default:
                            Console.WriteLine("Некорректный выбор! Можно ввести от 1 до 3");
                            Console.ReadKey();
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("Для выбора дальнейшего действия требуется ввести номер пункта меню.");
                    Console.ReadKey();
                }
            }
        }

        static void StartTesting()
        {
            do
            {
                var questions = QuestionStorage.GetAllQuestions();

                Console.Write("Напиши своё имя: ");
                string userName = Console.ReadLine() ?? "Неизвестный";

                var user = new User(userName);

                for (int i = 0; i < questions.Count; i++)
                {
                    Console.WriteLine($"Вопрос №{i + 1} \n{questions[i].text}");
                    (int userAnswer, int correctAnswer) = (GetUserAnswer(), questions[i].answer);
                    if (userAnswer == correctAnswer)
                        user.AddCorrectAnswer();
                }

                user.AddDiagnosis(questions.Count);

                UserResultsStorage.Save(user);

                Console.WriteLine($"{user.name}, твой диагноз - {user.diagnosis}");
            }
            while (RepeatAgain());
        }


        static int GetUserAnswer()
        {
            while (true)
            {
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.Write("Пожалуйста, введите число!");
                }
                catch (OverflowException)
                {
                    Console.Write("Число должно быть не более 2*10^9!");
                }
            }
        }
        static bool RepeatAgain()
        {
            Console.Write("Есть желание повторить тест? (да/нет): ");
            while (true)
            {
                string input = Console.ReadLine()?.ToLower() ?? string.Empty;
                if (input == "да") return true;
                else if (input == "нет") return false;
                else Console.Write("Некорректный ввод! Повторить тест? (да/нет): ");
            }
        }

        static void ShowResultTesting()
        {
            Console.Clear();
            Console.WriteLine($"{"Имя",-15}{"Правильные ответы",18}{"Диагноз",15}");
            var users = UserResultsStorage.GetAll();
            if (users.Any())
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.name,-15}{user.countCorrectAnswer,10}{user.diagnosis,23}");
                }
            }
            else
                Console.WriteLine("Результаты проведенных тестирований отсутствуют.");

            Console.WriteLine("Для возвращение в меню, нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
