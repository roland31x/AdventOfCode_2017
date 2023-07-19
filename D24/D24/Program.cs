namespace D24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bridge.Load(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(Bridge.OverallBest());
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Bridge.LongestBest());
        }
    }
    public static class Bridge
    {
        static List<Component> list = new List<Component>();
        public static void Load(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                while (!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Split('/');
                    list.Add(new Component(int.Parse(tokens[0]), int.Parse(tokens[1])));
                }
            }
        }
        public static int LongestBest()
        {
            List<List<Component>> valid = new List<List<Component>>();
            List<Component> current = new List<Component>();
            List<Component> start = list.Where(x => x.Con1 == 0 || x.Con2 == 0).ToList();
            foreach (Component st in start)
            {
                if (st.Con1 == 0)
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current.Add(st);
                    DFS(list.Where(x => (x != st) && (x.Con1 == st.Con2 || x.Con2 == st.Con2)).ToList(), st.Con2, ref valid, current);
                    current.Remove(st);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
                else
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current.Add(st);
                    DFS(list.Where(x => (x != st) && (x.Con1 == st.Con1 || x.Con2 == st.Con1)).ToList(), st.Con1, ref valid, current);
                    current.Remove(st);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
            }

            int best = 0;
            foreach(List<Component> v in valid)
            {
                int currentscore = 0;
                foreach (Component c in v)
                {
                    currentscore += c.Con1;
                    currentscore += c.Con2;
                }
                if(currentscore > best)
                    best = currentscore;
            }
            return best;
        }
        
        static void DFS(List<Component> left, int currentconval, ref List<List<Component>> valid, List<Component> current)
        {
            if (left.Count == 0)
            {
                List<Component> toadd = new List<Component>();
                foreach(Component c in current)
                {
                    toadd.Add(c);
                }
                if (valid.Any())
                {
                    if (valid[0].Count < toadd.Count)
                    {
                        valid.Clear();
                        valid.Add(toadd);
                    }
                    else if (valid[0].Count > toadd.Count)
                        return;
                    else
                    {
                        valid.Add(toadd);
                    }
                }
                else
                {
                    valid.Add(toadd);
                }
            }
            foreach (Component st in left)
            {
                if (st.Con1 == currentconval)
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current.Add(st);
                    DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con2 && !x.Con1Connected) || (x.Con2 == st.Con2 && !x.Con2Connected))).ToList(), st.Con2, ref valid, current);
                    current.Remove(st);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
                else
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current.Add(st);
                    DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con1 && !x.Con1Connected) || (x.Con2 == st.Con1 && !x.Con2Connected))).ToList(), st.Con1, ref valid, current);
                    current.Remove(st);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
            }
        }
        public static int OverallBest()
        {
            int score = 0;
            List<Component> start = list.Where(x => x.Con1 == 0 || x.Con2 == 0).ToList();
            foreach (Component st in start)
            {
                int current = 0;
                if (st.Con1 == 0)
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current += st.Con2;
                    current += DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con2 && !x.Con1Connected) || (x.Con2 == st.Con2 && !x.Con2Connected))).ToList(), st.Con2);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
                else
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current += st.Con1;
                    current += DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con1 && !x.Con1Connected) || (x.Con2 == st.Con1 && !x.Con2Connected))).ToList(), st.Con1);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
                if (current > score)
                    score = current;
            }

            return score;
        }
        static int DFS(List<Component> left, int currentconval) 
        {
            if (left.Count == 0)
                return 0;
            int score = 0;
            foreach (Component st in left)
            {
                int current = 0;
                if (st.Con1 == currentconval)
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current += st.Con1;
                    current += st.Con2;
                    current += DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con2 && !x.Con1Connected) || (x.Con2 == st.Con2 && !x.Con2Connected))).ToList(), st.Con2);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }
                else
                {
                    st.Con1Connected = true;
                    st.Con2Connected = true;
                    current += st.Con1;
                    current += st.Con2;
                    current += DFS(list.Where(x => (x != st) && ((x.Con1 == st.Con1 && !x.Con1Connected) || (x.Con2 == st.Con1 && !x.Con2Connected))).ToList(), st.Con1);
                    st.Con1Connected = false;
                    st.Con2Connected = false;
                }               
                if(current > score)
                    score = current;
            }
            return score;
        }
    }
    public class Component
    {
        public int Con1;
        public int Con2;
        public bool Con1Connected = false;
        public bool Con2Connected = false;
        public Component(int con1, int con2) 
        {
            this.Con1 = con1;
            this.Con2 = con2;
        }
        public override string ToString()
        {
            return Con1.ToString() + "/"+ Con2.ToString();
        }
    }
}