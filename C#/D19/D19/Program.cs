using System.Text;

namespace D19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(@"..\..\..\input.txt");
            Tuple<string,int> res = map.GetPath();
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(res.Item1); 
            Console.WriteLine("Part 2 solution"); 
            Console.WriteLine(res.Item2);
        }
    }
    public class Map
    {
        public static List<int[]> dirs = new List<int[]>()
        {
            new int[] { -1, 0 },
            new int[] { 0, 1 },
            new int[] { 1, 0 },
            new int[] { 0, -1 },
        };

        int[,] map;
        char[,] POI;
        Player player = new Player();
        public Map(string file)
        {
            string[] lines = File.ReadAllLines(file);
            int n = lines.Length;
            int m = lines[0].Length;
            map = new int[n, m];
            POI = new char[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (lines[i][j] == 32)
                        map[i,j] = 9;
                    else if (lines[i][j] == '|' || lines[i][j] == '+' || lines[i][j] == '-')
                        map[i, j] = 1;
                    else
                    {
                        map[i, j] = 1;
                        POI[i, j] = lines[i][j];
                    }                                             
                }
            }
        }
        public Tuple<string,int> GetPath()
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i < map.GetLength(1); i++)
            {
                if (map[0,i] == 1)
                {
                    player.I = 0;
                    player.J = i;
                    break;
                }    
            }
            bool canmove = true;
            int count = 0;
            while (canmove)
            {
                if (POI[player.I,player.J] > 0)
                    sb.Append(POI[player.I,player.J]);
                Move(player, ref canmove);
                count++;
            }

            return new Tuple<string, int>(sb.ToString(), count);
        }
        void Move(Player player, ref bool canmove)
        {
            int currentdir = player.dir;
            int newI = player.I + dirs[currentdir][0]; 
            int newJ = player.J + dirs[currentdir][1];
            if(newI < 0  || newJ < 0 || newI >= map.GetLength(0) || newJ >= map.GetLength(1))
            {

            }    
            else if(map[newI,newJ] == 1)               
            {
                player.I = newI;
                player.J = newJ;
                return;
            }

            for(int i = 0; i < 4; i++)
            {
                if (i == currentdir || (i + 2) % 4 == currentdir)
                    continue;
                player.dir = i;
                newI = player.I + dirs[player.dir][0];
                newJ = player.J + dirs[player.dir][1];
                if (newI < 0 || newJ < 0 || newI >= map.GetLength(0) || newJ >= map.GetLength(1))
                    continue;
                else if (map[newI, newJ] == 1)
                {
                    player.I = newI;
                    player.J = newJ;
                    return;
                }
            }
            canmove = false;
            
        }
    }
    public class Player
    {
        public int I;
        public int J;
        public int dir;
        public Player()
        {
            I = 0;
            J = 0;
            dir = 3;
        }
    }
}