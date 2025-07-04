namespace GeniyIdiotConsoleApp
{
    public class QuestionStorage
    {
        readonly static string _path = Path.Combine(Environment.CurrentDirectory, "questions.txt");

        public static List<Question> GetAll()
        {
            var lines = FileServices.Load(_path);

            var questions = new List<Question>();
            foreach (var line in lines)
            {
                var item = line.Split('#');
                (string textQuestion, int answerQuestion) = (item[0], Convert.ToInt32(item[1]));
                questions.Add(new Question(textQuestion, answerQuestion));
            }

            Shuffle(questions);

            return questions;
        }

        public static void Add(Question question)
        {
            throw new NotImplementedException();
        }

        static void Shuffle(List<Question> questions)
        {
            var shuffleQuestions = new List<Question>();
            Random random = new Random();
            for (int i = 0; i < questions.Count; i++)
            {
                int index = random.Next(questions.Count);
                shuffleQuestions.Add(questions[index]);
                questions.RemoveAt(index);
            }
            questions.AddRange(shuffleQuestions);
        }
    }
}
