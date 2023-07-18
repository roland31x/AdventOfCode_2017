namespace D13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FireWall[] fireWalls;
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            fireWalls = new FireWall[int.Parse(lines.Last().Split(':')[0]) + 1];
            foreach(string s in lines)
            {
                fireWalls[int.Parse(s.Split(':')[0])] = new FireWall(int.Parse(s.Split(' ')[1]));
            }
            List<Driver> drivers = new List<Driver>() { new Driver(0,0) };
            int dr = 0;
            int sum = 0;
            int time = 0;
            int rightstarttime = 0;
            bool found = false;
            while(!found) 
            {
                if(dr <  fireWalls.Length) 
                    if (fireWalls[dr] != null) 
                        if (fireWalls[dr].Scanner == 0)
                            sum += fireWalls[dr].Len * dr;
                drivers = drivers.Where(x => (fireWalls[x.Pos] == null ? true : fireWalls[x.Pos].Scanner != 0) ).ToList();
                foreach(Driver d in drivers)
                {
                    d.Pos++;
                    if (d.Pos == fireWalls.Length)
                    {
                        found = true;
                        rightstarttime = d.StartTime;
                    }
                        
                }
                TickFirewalls(fireWalls);
                time++;
                dr++;
                drivers.Add(new Driver(time, 0));
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sum);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(rightstarttime);
        }
        static void TickFirewalls(FireWall[] fireWalls)
        {
            foreach (FireWall f in fireWalls)
            {
                if (f != null)
                    f.Tick();
            }
        }
    }
    public class Driver
    {
        public int StartTime;
        public int Pos;
        public Driver(int startTime, int pos)
        {
            StartTime = startTime;
            Pos = pos;
        }
    }
    public class FireWall
    {
        int[] data;
        public int Len;
        public int Scanner = 0;
        int dir = 1;
        public FireWall(int len) 
        {
            data = new int[len];
            Len = len;
        }
        public void Tick()
        {
            if (Scanner == data.Length - 1)
                dir *= -1;
            Scanner += dir;
            if (Scanner == 0)
                dir *= -1;           
        }
    }
}