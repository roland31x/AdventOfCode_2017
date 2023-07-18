namespace D11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pathing path = new Pathing(@"..\..\..\input.txt");
            HexMap map = new HexMap(path);
            map.Run();
        }
    }
    public class HexMap
    {
        static Dictionary<string, int[]> dirs = new Dictionary<string, int[]>() { { "n", new int[] { 2, 0 } }, { "s", new int[] { -2, 0 } }, { "se", new int[] { -1, 1 } }, { "sw", new int[] { -1, -1 } }, { "ne", new int[] { 1, 1 } }, { "nw", new int[] { 1, -1 } }, };
        //HexData[,] map = new HexData[5001, 5001];
        //int mh { get { return map.GetLength(1) / 2; } }
        //int nh { get { return map.GetLength(0) / 2; } }
        Pathing path;
        HexData player;
        public HexMap(Pathing path)
        {
            this.path = path;
            //for (int i = 0; i < map.GetLength(0); i++)
            //    for (int j = 0; j < map.GetLength(1); j++)
            //        map[i, j] = new HexData(i, j, 0, 0);
            player = new HexData(0, 0, 0, 0);
        }
        public void Run()
        {
            int maxaway = 0;
            foreach(string s in path.Paths)
            {
                player = new HexData(player.I + dirs[s][0],player.J + dirs[s][1], player.Mark, player.Value);
                int check = (int)((decimal)Math.Abs(player.I) / 2 + (decimal)Math.Abs(player.J) / 2);
                if(check > maxaway)
                    maxaway = check;
            }
            int away = (int)((decimal)Math.Abs(player.I) / 2 + (decimal)Math.Abs(player.J) / 2);

            Console.WriteLine("Part 1 solution");
            Console.WriteLine(away);
            Console.WriteLine("Part 2 solution");
            Console.WriteLine(maxaway);

            //Queue<HexData> queue = new Queue<HexData>();
            //player.I += nh;
            //player.J += mh;
            
            //queue.Enqueue(player);
            //while (queue.Count > 0)
            //{
            //    HexData deq = queue.Dequeue();
            //    if (deq.I == nh && deq.J == mh)
            //    {
            //        Console.WriteLine(deq.Mark);
            //        break;
            //    }
                
            //    foreach (int[] value in dirs.Values)
            //    {
            //        HexData toadd = new HexData(deq.I + value[0], deq.J + value[1], deq.Mark + 1, deq.Value);
            //        bool ok = true;
            //        try
            //        {
            //            if (map[toadd.I, toadd.J] != null)
            //                ok = false;
            //            if (ok)
            //            {
            //                queue.Enqueue(toadd);
            //                map[toadd.I, toadd.J] = toadd;
            //            }
            //        }
            //        catch(IndexOutOfRangeException)
            //        {
            //            continue;
            //        }
                        
            //    }
            //}
        }

    }
    public class HexData
    {
        public int I;
        public int J;
        public int Mark;
        public int Value;
        public HexData(int i, int j, int mark, int value)
        {
            I = i;
            J = j;
            Mark = mark;
            Value = value;
        }
    }
    public class Pathing
    {
        public List<string> Paths = new List<string>();
        public Pathing(string file)
        {
            string line = File.ReadAllText(file);
            string[] p = line.Split(',');
            foreach(string s in p) 
            {
                Paths.Add(s);
            }
        }
    }
}