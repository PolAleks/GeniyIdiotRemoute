namespace GeniyIdiotConsoleApp
{
    public class User
    {
        public string name;
        public int countCorrectAnswer;
        public string diagnosis;

        public User(string name)
        {
            this.name = name;
            countCorrectAnswer = 0;
            diagnosis = "отсутствует";
        }

        public User(string name, int countCorrectAnswer, string diagnosis) : this(name)
        {
            this.countCorrectAnswer = countCorrectAnswer;
            this.diagnosis = diagnosis;
        }

        public void AddCorrectAnswer() => countCorrectAnswer++;

        public void AddDiagnosis(int countQuestions)
        {
            double percentage = ((double)countCorrectAnswer / countQuestions) * 100;

            diagnosis = percentage switch
            {
                100 => "Гений",
                >= 80 => "Талант",
                >= 60 => "Нормальный",
                >= 40 => "Дурак",
                >= 20 => "Кретин",
                _ => "Идиот"
            };
        }

        public override string ToString() => $"{name}#{countCorrectAnswer}#{diagnosis}";
    }
}
