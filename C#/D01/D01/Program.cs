namespace D01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"..\..\..\input.txt");
            int sum1 = 0;
            int sum2 = 0;
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == input[(i + 1) % input.Length])
                    sum1 += input[i] - '0';
                if (input[i] == input[(i + input.Length / 2) % input.Length])
                    sum2 += input[i] - '0';
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sum1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(sum2);
        }
    }
}