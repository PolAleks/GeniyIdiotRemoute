namespace GeniyIdiotConsoleApp
{
    public class UserResultsStorage
    {
        public static void Save(User user)
        {
            string file = "log.txt";
            string content = $"{user.name}#{user.countCorrectAnswer}#{user.diagnosis}";

            FileServices.Save(file, content);
        }

        public static List<User> GetAll()
        {
            var users = new List<User>();
            var file = "log.txt";

            foreach(var line in FileServices.Load(file)) 
            {
                var item = line.Split('#');
                (string name, int countCorrectAnswer, string diagnosis) = (item[0], Convert.ToInt32(item[1]), item[2]);
                users.Add(new User(name, countCorrectAnswer, diagnosis));
            }
        
            return users;
        }
}
}
