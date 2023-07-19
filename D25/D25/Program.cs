using System.Reflection;
using System.Text.RegularExpressions;

namespace D25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Machine turing = new Machine();
            int amountofruns = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                string InitialMachine = sr.ReadLine()!.Split(' ').Last().Replace(".","");
                amountofruns = int.Parse(sr.ReadLine()!.Split(' ')[5]);
                sr.ReadLine();
                while(!sr.EndOfStream) 
                {
                    string[] stateinfo = new string[9];
                    for(int i = 0; i < 9; i++)
                    {
                        stateinfo[i] = sr.ReadLine()!.Trim();
                    }
                    string statename = stateinfo[0].Split(' ')[2].Replace(":", "");
                    State toadd = new State(statename);
                    toadd.tapeOp[0] = int.Parse(Regex.Match(stateinfo[2], @"[0-9]").Value);
                    toadd.dir[0] = stateinfo[3].Contains("left") ? -1 : 1;
                    toadd.state[0] = stateinfo[4].Split(' ').Last().Replace(".", "");



                    toadd.tapeOp[1] = int.Parse(Regex.Match(stateinfo[6], @"[0-9]").Value);
                    toadd.dir[1] = stateinfo[7].Contains("left") ? -1 : 1;
                    toadd.state[1] = stateinfo[8].Split(' ').Last().Replace(".", "");

                    sr.ReadLine();
                }
                turing.current = State.GetState(InitialMachine);
            }
            for(int i = 0; i < amountofruns; i++)
            {
                turing.Run();
            }
            Console.WriteLine("Final solution:");
            Console.WriteLine(turing.Checksum());
        }
    }
    public class Machine
    {
        public State current;
        public int[] tape = new int[100000];
        int dir = 0;
        int driver;
        public Machine()
        {
            driver = tape.Length / 2;
        }
        public Machine(State state)
        {
            current = state;
            driver = tape.Length / 2;
        }
        public void Run()
        {
            int currentval = tape[driver];
            tape[driver] = current.tapeOp[currentval];
            dir = current.dir[currentval];
            current = State.GetState(current.state[currentval]);
            driver += dir;
        }
        public int Checksum()
        {
            int count = 0;
            foreach (int i in tape)
                if (i == 1)
                    count++;
            return count;
        }
    }
    public class State
    {
        public static List<State> states = new List<State>();
        public static State GetState(string state)
        {
            foreach(State s in states)
            {
                if(s.Name == state)
                    return s;
            }
            throw new InvalidDataException();
        }
        public string Name;
        // storing operations in a 2 item array, because we act wether value is 0, we take info from index 0, if it's 1 we take info from index 1
        public int[] tapeOp = new int[2];
        public int[] dir = new int[2];
        public string[] state = new string[2];
        public State(string Name) 
        {
            this.Name = Name;
            states.Add(this);
        }
    }
}