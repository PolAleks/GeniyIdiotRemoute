namespace GeniyIdiotConsoleApp
{
    public class QuestionStorage
    {
        public static List<Question> GetAll()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "questions.txt");
            var lines = FileServices.Load(path);

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

        public static void Add()
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
