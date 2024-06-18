namespace D05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> maze = new List<int>();
            List<int> maze2 = new List<int>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    int toadd = int.Parse(sr.ReadLine()!);
                    maze.Add(toadd);
                    maze2.Add(toadd);
                }
            }
            int steps = 0;
            int driver = 0;
            while(driver < maze.Count)
            {
                int tojump = maze[driver];
                maze[driver]++;
                driver = driver + tojump;
                steps++;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(steps);

            steps = 0;
            driver = 0;
            while(driver < maze2.Count) 
            {
                int tojump = maze2[driver];
                if (tojump >= 3)
                    maze2[driver]--;
                else
                    maze2[driver]++;
                driver = driver + tojump;
                steps++;
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(steps);
        }
    }
}