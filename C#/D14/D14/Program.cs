namespace D14
{
    internal class Program
    {
        public static Dictionary<char, int> Hex = new Dictionary<char, int>() { { '0', 0 }, {'1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 }, { 'A', 10 }, { 'B', 11 }, { 'C', 12 }, { 'D', 13 }, { 'E', 14 }, { 'F', 15 } };
        static void Main(string[] args)
        {
            string input = "amgozmfv";
            int[,] disk = new int[128, 128];
            int count = 0;
            for(int i = 0; i < 128; i++)
            {
                string tocompute = input + "-" + i.ToString();
                string hash = KnotHash(tocompute);
                for(int k = 0; k < hash.Length; k++)
                {
                    int toadd = Hex[hash[k]];
                    Stack<int> stack = new Stack<int>();
                    while(toadd > 0)
                    {
                        stack.Push(toadd % 2);
                        toadd /= 2;
                    }
                    while (stack.Count < 4)
                        stack.Push(0);
                    int j = 0;
                    while(stack.Count > 0)
                    {
                        disk[i, k * 4 + j] = stack.Pop();
                        if (disk[i, k * 4 + j] == 1)
                            count++;
                        j++;
                    }
                        
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(count);

            int groups = 0;
            for(int i = 0; i < disk.GetLength(0); i++)
            {
                for(int j = 0; j < disk.GetLength(1); j++)
                {
                    if (disk[i, j] == 1)
                    {
                        DFS(i, j, disk);
                        groups++;
                    }                     
                }
            }

            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(groups);
        }
        static void DFS(int i, int j, int[,] mat)
        {
            if (i < 0 || i >= mat.GetLength(0) || j < 0 || j >= mat.GetLength(1))
                return;
            if (mat[i, j] != 1)
                return;
            mat[i, j] = 2;
            DFS(i + 1, j, mat);
            DFS(i - 1, j, mat);
            DFS(i, j + 1, mat);
            DFS(i, j - 1, mat);
        }
        static string KnotHash(string input)
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