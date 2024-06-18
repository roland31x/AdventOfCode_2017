using System.Text;

namespace D10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText(@"..\..\..\input.txt");
            List<int> lengths = new List<int>();
            foreach(string s in input.Split(','))
            {
                lengths.Add(int.Parse(s));
            }
            List<int> hash = new List<int>();
            for (int i = 0; i < 256; i++)
                hash.Add(i);
            int driver = 0;
            int stepsize = 0;
            for(int i = 0; i < lengths.Count; i++)
            {
                List<int> reversed = new List<int>();
                int len = lengths[i];
                for(int j = driver; j < driver + len; j++)
                {
                    reversed.Add(hash[j % hash.Count]);
                }
                reversed.Reverse();
                for(int j = 0; j < len; j++)
                {
                    hash[(driver + j) % hash.Count] = reversed[j];
                }
                driver += len;
                driver += stepsize;
                driver %= hash.Count;
                stepsize++;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(hash[0] * hash[1]);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Hash(input).ToLower());


        }
        static string Hash(string input)
        {
            List<int> hash = new List<int>();
            for (int i = 0; i < 256; i++)
                hash.Add(i);

            List<int> actual = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                actual.Add(input[i]);
            }
            actual.Add(17);
            actual.Add(31);
            actual.Add(73);
            actual.Add(47);
            actual.Add(23);

            int driver = 0;
            int stepsize = 0;
            for (int times = 0; times < 64; times++)
            {
                for (int i = 0; i < actual.Count; i++)
                {
                    List<int> reversed = new List<int>();
                    int len = actual[i];
                    for (int j = driver; j < driver + len; j++)
                    {
                        reversed.Add(hash[j % hash.Count]);
                    }
                    reversed.Reverse();
                    for (int j = 0; j < len; j++)
                    {
                        hash[(driver + j) % hash.Count] = reversed[j];
                    }
                    driver += len;
                    driver += stepsize;
                    driver %= hash.Count;
                    stepsize++;
                }
            }
            List<int> dense = new List<int>();
            for (int times = 0; times < 16; times++)
            {
                int xor = hash[times * 16];
                for (int i = times * 16 + 1; i < times * 16 + 16; i++)
                {
                    xor = xor ^ hash[i];
                }
                dense.Add(xor);
            }

            string tor = "";
            for (int i = 0; i < dense.Count; i++)
            {
                tor += dense[i].ToString("X2");
            }
            return tor;
        }
    }
}