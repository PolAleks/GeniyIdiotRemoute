namespace GeniyIdiotConsoleApp
{
    public class QuestionStorage
    {
        readonly static string _file = "questions.txt";

        public static List<Question> GetAll()
        {
            var questions = new List<Question>();

            if (FileServices.Exists(_file))
            {

                var lines = FileServices.Load(_file);

                foreach (var line in lines)
                {
                    var item = line.Split('#');
                    (string textQuestion, int answerQuestion) = (item[0], Convert.ToInt32(item[1]));
                    questions.Add(new Question(textQuestion, answerQuestion));
                }
            }
            else
            {
                questions = [new Question("Сколько будет два плюс два умноженное на два?", 6),
                             new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9),
                             new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25),
                             new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60),
                             new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2),];

                foreach (var question in questions)
                {
                    Add(question);
                }
            }

            Shuffle(questions);

            return questions;
        }

        public static void Add(Question question)
        {
            FileServices.Save(_file, question.ToString());
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

        internal static void Delete(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
