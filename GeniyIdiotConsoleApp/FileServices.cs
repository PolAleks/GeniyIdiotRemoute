namespace GeniyIdiotConsoleApp
{
    public class FileServices
    {
        public static List<string> Load(string file)
        {
            var path = Path.Combine(Environment.CurrentDirectory, file);
            var lines = new List<string>();

            if (File.Exists(path))
            {
                lines = File.ReadAllLines(path).ToList();
            }

            return lines;
        }

        public static void Save(string file, string content)
        {
            var path = Path.Combine(Environment.CurrentDirectory, file);

            using (StreamWriter sr = new StreamWriter(path, true, System.Text.Encoding.Default))
            {
                sr.WriteLine(content);
            }
        }

        public static bool Exists(string file)
        {
            return File.Exists(file);
        }

        public static void Delete(string file, Question question)
        {
            if (Exists(file))
            {
                var lines = File.ReadAllLines(file).ToList();
                lines.RemoveAll(q => q.Equals(question.ToString()));
                File.WriteAllLines(file, lines);
            }
        }
    }
}
