namespace D16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<char> programs = new List<char>();
            List<char> initial = new List<char>();
            for(int i = 0 + 'a'; i <= 0 + 'p'; i++)
            {
                initial.Add(Convert.ToChar(i));
                programs.Add(Convert.ToChar(i));
            }
            string[] commands = File.ReadAllText(@"..\..\..\input.txt").Split(',');
            bool found = false;
            int steps = 0;
            do
            {
                steps++;
                foreach (string command in commands)
                {
                    ExecCommand(ref programs, command);                  
                }
                if(steps == 1)
                {
                    Console.WriteLine("Part 1 solution:");
                    foreach (char c in programs)
                        Console.Write(c);
                    Console.WriteLine();
                }
                for (int i = 0; i < programs.Count; i++)
                {
                    if (programs[i] != initial[i])
                        break;
                    if (i == programs.Count - 1)
                    {
                        found = true;
                    }
                }
                
            } while (!found);
            
            for(int times = 0; times < 1_000_000_000 % steps; times++)
            {
                foreach (string command in commands)
                {
                    ExecCommand(ref programs, command);
                }
            }

            Console.WriteLine("Part 2 solution:");
            foreach (char c in programs)
                Console.Write(c);
            Console.WriteLine();
        }
        static void ExecCommand(ref List<char> programs, string command) 
        {
            switch (command[0])
            {
                case 's':
                    Spin(ref programs, int.Parse(command.Replace("s", "")));
                    break;
                case 'x':
                    SwapIdx(ref programs, command.Replace("x",""));
                    break;
                case 'p':
                    SwapPrograms(ref programs, command);
                    break;
            }
        }

        static void SwapPrograms(ref List<char> programs, string command)
        {
            char leftidx = command.Split('/')[0][1];
            char rightidx = command.Split('/')[1][0];
            int lidx = programs.IndexOf(leftidx);
            int ridx = programs.IndexOf(rightidx);
            (programs[lidx], programs[ridx]) = (programs[ridx], programs[lidx]);
        }
        static void SwapIdx(ref List<char> programs, string command)
        {
            int leftidx = int.Parse(command.Split('/')[0]);
            int rightidx = int.Parse(command.Split('/')[1]);
            (programs[leftidx], programs[rightidx]) = (programs[rightidx], programs[leftidx]);
        }

        static void Spin(ref List<char> programs, int amount) 
        {
            List<char> newp = new List<char>();
            for(int i = programs.Count - amount; i < programs.Count + programs.Count - amount; i++)
            {
                newp.Add(programs[i % programs.Count]);
            }
            programs = newp;

        }
    }
}