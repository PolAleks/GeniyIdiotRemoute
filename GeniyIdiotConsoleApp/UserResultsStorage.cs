namespace GeniyIdiotConsoleApp
{
    public class UserResultsStorage
    {
        public static void Save(User user)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "log.txt");
            using (StreamWriter sw = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"{user.name}#{user.countCorrectAnswer}#{user.diagnosis}");
            }
        }

        public static List<User> GetAll()
        {
            var users = new List<User>();
            string path = Path.Combine(Environment.CurrentDirectory, "log.txt");
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine().Split('#');
                        (string name, int countCorrectAnswer, string diagnosis) = (line[0], Convert.ToInt32(line[1]), line[2]);
                        users.Add(new User(name, countCorrectAnswer, diagnosis));
                    }
                }
            }
            return users;
        }
    }
}
