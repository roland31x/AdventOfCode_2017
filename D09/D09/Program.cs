namespace D09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line = File.ReadAllText(@"..\..\..\input.txt");
            int driver = 0;
            int level = 0;
            int sum = 0;
            int garbage = 0;
            while(driver < line.Length) 
            {
                if (line[driver] == '!')
                {
                    driver += 2;
                    continue;
                }
                if (line[driver] == '{')
                    level++;
                if (line[driver] == '}')
                {
                    sum += level;
                    level--;
                }
                if (line[driver] == '<')
                {
                    driver++;
                    while (true)
                    {
                        if (line[driver] == '!')
                        {
                            driver += 2;
                            continue;
                        }
                        garbage++;
                        if (line[driver] == '>')
                        {
                            garbage--;
                            break;
                        }                           
                        driver++;
                    }
                }
                driver++;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sum);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(garbage);
        }
    }
}