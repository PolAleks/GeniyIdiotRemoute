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
                Console.Write("Выберите 1 или 2: ");
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
                        default:
                            Console.WriteLine("Некорректный выбор! Можно ввести только 1 или 2.");
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

                RecordingDiagnoseInLogFile(user);

                Console.WriteLine($"{user.name}, твой диагноз - {user.diagnosis}");
            }
            while (RepeatAgain());
        }

        static void RecordingDiagnoseInLogFile(User user)
        {
            string pathToFolder = Environment.CurrentDirectory;
            string nameLogFile = Path.Combine(pathToFolder, "log.txt");

            using (StreamWriter sw = new StreamWriter(nameLogFile, true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{user.name}#{user.countCorrectAnswer}#{user.diagnosis}");
            }
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

            string pathToFolder = Environment.CurrentDirectory;
            string nameLogFile = Path.Combine(pathToFolder, "log.txt");

            if (File.Exists(nameLogFile))
            {
                using (StreamReader sr = new StreamReader(nameLogFile))
                {
                    Console.WriteLine($"{"Имя",-15}{"Правильные ответы",18}{"Диагноз",15}");
                    
                    while (!sr.EndOfStream)
                    {
                        var data = sr.ReadLine().Split('#');
                        (string nameUser, int countCorrectAnswer, string diagnose) = (data[0], Convert.ToInt32(data[1]), data[2]);
                        Console.WriteLine($"{nameUser,-15}{countCorrectAnswer,10}{diagnose,23}");
                    }
                }
            }
            else Console.WriteLine("Результаты проведенных тестирований отсутствуют.");

            Console.WriteLine("Для возвращение в меню, нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
