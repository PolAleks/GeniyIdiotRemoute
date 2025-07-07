namespace GeniyIdiotConsoleApp
{
    public class UserResultsStorage
    {
        readonly static string _file = "log.txt";
        public static void Save(User user)
        {
            FileServices.Save(_file, user.ToString());
        }

        public static List<User> GetAll()
        {
            var users = new List<User>();
            
            foreach (var line in FileServices.Load(_file))
            {
                var item = line.Split('#');
                (string name, int countCorrectAnswer, string diagnosis) = (item[0], Convert.ToInt32(item[1]), item[2]);
                users.Add(new User(name, countCorrectAnswer, diagnosis));
            }

            return users;
        }
    }
}
