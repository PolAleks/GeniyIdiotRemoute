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

                string diagnose = GetDiagnoses(countCorrectAnswer, answers.Length);

                RecordingDiagnoseInLogFile(userName, countCorrectAnswer, diagnose);

                Console.WriteLine($"{userName}, твой диагноз - {diagnose}");
            }
            while (RepeatAgain());
        }

        static void RecordingDiagnoseInLogFile(string name, int countCorrectAnswer, string diagnose)
        {
            if (string.IsNullOrEmpty(diagnose)) diagnose = "отсутствует";

            string pathToFolder = Environment.CurrentDirectory;
            string nameLogFile = Path.Combine(pathToFolder, "log.txt");

            bool isFirstRecord = false;
            if (!File.Exists(nameLogFile))
            {
                isFirstRecord = true;
            }

            using (StreamWriter sw = new StreamWriter(nameLogFile, true, System.Text.Encoding.Default))
            {
                if (isFirstRecord) sw.WriteLine($"{"ФИО",-25}{"Кол-во ответов",-20}{"Диагноз",-15}");
                sw.WriteLine($"{name,-25}{countCorrectAnswer,-20}{diagnose,-15}");
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

        static string GetDiagnoses(int countCorrectAnswer, int countAnswer)
        {
            if (countCorrectAnswer == 0) return "Идиот";

            double percentage = ((double)countCorrectAnswer / countAnswer) * 100;

            return percentage switch
            {
                100 => "Гений",
                >= 80 => "Талант",
                >= 60 => "Нормальный",
                >= 40 => "Дурак",
                >= 20 => "Кретин",
                _ => "Идиот"
            };
        }

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

            int[] answers = [6, 9, 25, 60, 2];

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

        static void ShowResultTesting()
        {
            Console.Clear();

            string pathToFolder = Environment.CurrentDirectory;
            string nameLogFile = Path.Combine(pathToFolder, "log.txt");
            
            if (File.Exists(nameLogFile))
            {
                using (StreamReader sr = new StreamReader(nameLogFile))
                {
                    while (!sr.EndOfStream)
                    {
                        Console.WriteLine(sr.ReadLine());
                    }
                }
            }
            else Console.WriteLine("Результаты проведенных тестирований отсутствуют.");

            Console.WriteLine("Для возвращение в меню, нажмите любую клавишу.");
            Console.ReadKey();
        }
    }
}
