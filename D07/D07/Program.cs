namespace D07
{
    internal class Program
    {
        static List<Node> tocheck = new List<Node>();
        static void Main(string[] args)
        {
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine()!;
                    string mainnodename = buffer.Split(' ')[0];
                    int value = int.Parse(buffer.Split(' ')[1].Replace("(","").Replace(")",""));
                    Node main = Node.GetNode(mainnodename);
                    main.Value = value;
                    if (buffer.Contains("->"))
                    {
                        string secondary = buffer.Split("->")[1];
                        string[] othernodes = secondary.Split(",");
                        foreach(string s in othernodes)
                        {
                            Node child = Node.GetNode(s.Trim());
                            main.children.Add(child);
                            child.Parent = main;
                        }
                    }
                }
            }
            Node start = Node.GetHighest();
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(start.Name);
            Node.CalcScores();
            Console.WriteLine("Part 2 solution");
            DFS(start);
            Console.WriteLine();
        }
        static void DFS(Node current)
        {
            if(current.children.Count == 0)
            {
                return;
            }
            List<int> scores = new List<int>();
            foreach(Node node in current.children) 
            {
                scores.Add(node.Score);
            }
            bool ok = true;
            for(int i = 0; i < scores.Count - 1; i++) 
            {
                if (scores[i] != scores[i + 1])
                {
                    ok = false; 
                    break;
                }                  
            }           
            
            if(!ok)
            {
                int idx = 0;
                bool rightidx = false;
                do
                {
                    List<int> left = new List<int>();
                    for (int i = 0; i < scores.Count; i++)
                    {
                        if (i == idx)
                            continue;
                        left.Add(scores[i]);
                    }
                    int scoreremoved = scores[idx];
                    for (int i = 0; i < left.Count; i++)
                    {
                        if (scoreremoved == left[i])
                            break;
                        if (i == left.Count - 1)
                            rightidx = true;
                    }
                    if (!rightidx)
                        idx++;
                } while (!rightidx);

                DFS(current.children[idx]);
            }
            else
            {
                int currentval = current.Value;
                int currentscore = current.Score;
                int neededscore = current.Parent.children.Where(x => x != current).First().Score;
                int diff = currentval + (neededscore - currentscore);
                Console.WriteLine(diff);
            }
        }
    }
    public class Node
    {
        public static List<Node> All = new List<Node>();
        public static Node GetNode(string name)
        {
            foreach(Node node in All) 
            {
                if(node.Name == name) return node;
            }
            return new Node(0, name);
        }
        public static void CalcScores()
        {
            foreach (Node node in All)
            {
                node.Score = node.CalcScore();
            }
        }
        public static Node GetHighest()
        {
            Node current = All[0];
            while(current.Parent != null)
            {
                current = current.Parent;
            }
            return current;
        }

        public List<Node> children = new List<Node>();
        public Node Parent;
        public int Value { get; set; }
        public int Score { get; set; }
        public string Name { get; set; }
        public Node(int Val, string name) 
        {
            Name = name;
            Value = Val;
            All.Add(this);            
        }
        public int CalcScore()
        {
            int sum = Value;
            foreach(Node node in children)
            {
                sum += node.CalcScore();
            }
            return sum;
        }
        public override string ToString()
        {
            return Score.ToString();
        }
    }
}