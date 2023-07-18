using System.Text.RegularExpressions;

namespace D20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Particle> particles = Particle.Parse(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(Particle.LeastAccelerating(particles));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Particle.Simulate(particles));
        }
    }
    public class Particle
    {
        static Regex posregex = new Regex(@"p=<-?[0-9]+,-?[0-9]+,-?[0-9]+>");
        static Regex velregex = new Regex(@"v=<-?[0-9]+,-?[0-9]+,-?[0-9]+>");
        static Regex accregex = new Regex(@"a=<-?[0-9]+,-?[0-9]+,-?[0-9]+>");
        public static List<Particle> Parse(string file)
        {
            List<Particle> particles = new List<Particle>();
            using(StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string buffer = reader.ReadLine()!;
                    int[] p = new int[3];
                    int[] v = new int[3];
                    int[] a = new int[3];
                    string pos = posregex.Match(buffer).Value;
                    string vel = velregex.Match(buffer).Value;
                    string acc = accregex.Match(buffer).Value;
                    pos = pos.Replace("p=<", "").Replace(">", "");
                    vel = vel.Replace("v=<", "").Replace(">", "");
                    acc = acc.Replace("a=<", "").Replace(">", "");
                    string[] ptok = pos.Split(',');
                    string[] vtok = vel.Split(',');
                    string[] atok = acc.Split(',');
                    for(int i = 0; i < 3; i++)
                    {
                        p[i] = int.Parse(ptok[i]);
                        v[i] = int.Parse(vtok[i]);
                        a[i] = int.Parse(atok[i]);
                    }
                    particles.Add(new Particle(p, v, a, particles.Count - 1));
                }             
            }
            return particles;
        }
        public static int LeastAccelerating(List<Particle> particles)
        {
            int tor = 0;
            decimal dist = Dist(particles[0].a);
            for(int i = 0; i < particles.Count; i++) 
            {
                decimal check = Dist(particles[i].a);
                if(check <= dist)
                {
                    if(check == dist)
                    {
                        if (Dist(particles[i].v) <= Dist(particles[tor].v))
                        {
                            if (Dist(particles[i].v) == Dist(particles[tor].v))
                            {
                                if (Dist(particles[i].p) <= Dist(particles[tor].p))
                                {
                                    dist = check;
                                    tor = i;
                                }
                            }
                            else
                            {
                                dist = check;
                                tor = i;
                            }
                        }                      
                    }
                    else
                    {
                        dist = check;
                        tor = i;
                    }                  
                }
            }
            return tor;
        }
        public static int Simulate(List<Particle> particles)
        {
            for(int i = 0; i < 10000; i++)
            {
                particles = CheckCollision(particles);
                foreach(Particle particle in particles)
                {
                    particle.Tick();
                }
            }
            return particles.Count;
        }
        static List<Particle> CheckCollision(List<Particle> particles)
        {
            int[] notok = new int[particles.Count];
            for(int i = 0; i < particles.Count; i++)
            {
                if (notok[i] == 1)
                    continue;              
                for(int j = i + 1; j < particles.Count; j++)
                {
                    bool good = true;
                    if (notok[j] == 1)
                        continue;
                    for(int k = 0; k < 3; k++)
                    {
                        if (particles[i].p[k] != particles[j].p[k])
                            break;
                        if (k == 2)
                            good = false;
                    }
                    if (!good)
                    {
                        notok[i] = 1;
                        notok[j] = 1;
                    }                       
                }
            }
            List<Particle> newlist = new List<Particle>();
            for(int i = 0; i < particles.Count; i++)
            {
                if (notok[i] == 0)
                    newlist.Add(particles[i]);
            }
            return newlist;
        }
        static decimal Dist(int[] vector3d)
        {
            return vector3d[0] * vector3d[0] + vector3d[1] * vector3d[1] + vector3d[2] * vector3d[2];
        }

        public int[] p;
        public int[] v;
        public int[] a;
        public int idx;
        public Particle(int[] p, int[] v, int[] a, int index) 
        {
            this.p = p;
            this.v = v;
            this.a = a;
            idx = index;
        }
        void Tick()
        {
            for(int i = 0; i < 3; i++)
            {
                v[i] += a[i];
                p[i] += v[i];
            }
        }
    }
}