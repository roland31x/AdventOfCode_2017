using System.ComponentModel.Design;

namespace D22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map myMap = new Map(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(myMap.Activity(10000, false));
            Map myMap2 = new Map(@"..\..\..\input.txt");
            Console.WriteLine("Part 2 solution");
            Console.WriteLine(myMap2.Activity(10_000_000, true));

        }
    }
    public class Map
    {
        static List<int[]> dirs = new List<int[]>()
        {
            new int[] { 1, 0 },
            new int[] { 0, 1 },
            new int[] { -1, 0 },
            new int[] { 0, -1 },
        };
        static int size = 1000;

        Node[,] nodes = new Node[size, size]; // size 1k should be enough, should never go out of bounds

        List<Node> infected = new List<Node>(); // list is faster for little movement

        Node current;
        int _d;
        int dir { get { return _d; } set { _d = value; if(_d < 0) { _d += 4; } else if(_d >= 4 ) { _d -= 4; } } }

        public Map(string file)
        {
            int middleI = -1;
            int middleJ = -1;
            using (StreamReader sr = new StreamReader(file))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    for (int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == '#')
                            infected.Add(new Node(i, j, 2));
                        if (middleJ == -1 && j == line.Length - 1)
                            middleJ = j / 2;
                    }
                    i--;
                }
                middleI = i / 2;
            }
            foreach (Node node in infected)
            {
                if (node.I == middleI && node.J == middleJ)
                    current = node;
                nodes[node.I + size / 2, node.J + size / 2] = node;
            }
                
            if (current == null)
                current = new Node(middleI, middleJ);
        }
        public int Activity(int times, bool part2)
        {
            int count = 0;
            for (int i = 0; i < times; i++)
            {
                dir += GetDir(current.State);

                if (part2)
                    current.State++;
                else
                    current.State += 2;
                if (current.State == 2)
                    count++;

                nodes[current.I + size / 2, current.J + size / 2] = current;

                int nextI = current.I + dirs[dir][0];
                int nextJ = current.J + dirs[dir][1];
                if (nodes[nextI + size / 2, nextJ + size / 2] != null)
                {
                    current = nodes[nextI + size / 2, nextJ + size / 2];
                }
                else
                {
                    current = new Node(nextI, nextJ);
                }                 
            }
            return count;
        }
        int GetDir(int state)
        {
            switch (state)
            {
                case 0:
                    return -1;
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 3:
                    return 2;
            }
            throw new Exception("invalid state");
        }
    }
    public class Node
    {
        public int _s = 0;
        public int State { get { return _s; } set { _s = value; if (_s >= 4) { _s %= 4; } } }      // 0 for clean, 1 for weakened, 2 for infected, 3 for flagged
        public int I;
        public int J;
        public Node(int i, int j)
        {
            I = i; 
            J = j;
        }
        public Node(int i, int j, int infected)
        {
            I = i;
            J = j;
            State = infected;
        }
    }
}