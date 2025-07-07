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
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        lines.Add(sr.ReadLine());
                    }
                }
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
    }
}
