namespace D12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node.Parse(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(Node.GetNodeCount(Node.GetNode(0)));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Node.GetGroupCount());
        }
    }
    public class Node
    {
        public static void Parse(string file)
        {
            using (StreamReader sr = new StreamReader(file)) 
            {
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    Node con1 = GetNode(int.Parse(line.Split("<->")[0].Trim()));
                    string[] other = line.Split("<->")[1].Split(',');
                    foreach(string s in other) 
                    {
                        Node con2 = GetNode(int.Parse(s.Trim()));
                        con1.connections.Add(con2);
                        con2.connections.Add(con1);
                    }
                }
            }
        }
        public static int GetGroupCount()
        {
            foreach (Node n in Nodes.Values)
                n.wasMarked = false;

            int count = 0;
            foreach(Node n in Nodes.Values)
            {
                if (!n.wasMarked)
                {
                    GetNodeCount(n);
                    count++;
                }                  
            }
            return count;
        }
        public static int GetNodeCount(Node start)
        {
            int count = 0;
            start.wasMarked = true;
            count++;
            foreach(Node con in start.connections) 
            {
                if (!con.wasMarked)
                    count += GetNodeCount(con);
            }
            return count;
            
        }
        public static Dictionary<int,Node> Nodes = new Dictionary<int,Node>();
        public static Node GetNode(int id)
        {
            if (Nodes.TryGetValue(id, out Node node))
                return node;
            else
                return new Node(id);
        }
        public List<Node> connections = new List<Node>();
        public int ID;
        public bool wasMarked;
        public Node(int id) 
        {
            ID = id;
            Nodes.Add(id, this);
        }
    }
}