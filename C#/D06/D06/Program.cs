using System.Runtime.CompilerServices;

namespace D06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line = File.ReadAllText(@"..\..\..\input.txt");
            line = line.Replace('\t', ' ');
            string[] tokens = line.Split(' ');
            int[] banks = new int[tokens.Length];
            for(int i = 0; i < tokens.Length; i++)
            {
                banks[i] = int.Parse(tokens[i]);
            }
            bool done = false;
            int steps = 0;
            List<int[]> states = new List<int[]>();
            while (!done)
            {               
                states.Add(Clone(banks));
                int maxidx = FindMax(banks);
                int amount = banks[maxidx];
                banks[maxidx] = 0;
                while (amount > 0)
                {
                    maxidx++;
                    banks[maxidx % banks.Length]++;
                    amount--;
                }
                steps++;
                foreach (int[] state in states)
                {
                    for (int i = 0; i < state.Length; i++)
                    {
                        if (state[i] != banks[i])
                            break;
                        if (i == state.Length - 1)
                            done = true;
                    }
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(steps);
            states.Clear();

            steps = 0;
            done = false;
            while (!done)
            {
                states.Add(Clone(banks));
                int maxidx = FindMax(banks);
                int amount = banks[maxidx];
                banks[maxidx] = 0;
                while (amount > 0)
                {
                    maxidx++;
                    banks[maxidx % banks.Length]++;
                    amount--;
                }
                steps++;
                foreach (int[] state in states)
                {
                    for (int i = 0; i < state.Length; i++)
                    {
                        if (state[i] != banks[i])
                            break;
                        if (i == state.Length - 1)
                            done = true;
                    }
                }
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(steps);

        }
        static int FindMax(int[] banks) 
        {
            int idx = 0;
            for(int i = 1; i < banks.Length; i++) 
            {
                if (banks[i] > banks[idx])
                    idx = i;
            }
            return idx;
        }
        static int[] Clone(int[] array) 
        {
            int[] tor = new int[array.Length];
            for(int i = 0; i < array.Length; i++)
                tor[i] = array[i];
            return tor;
        }
    }
}