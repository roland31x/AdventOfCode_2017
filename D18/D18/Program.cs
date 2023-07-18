using Microsoft.Win32;
using System.Diagnostics;

namespace D18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            MyProgram test = new MyProgram(lines);
            test.Run();

            MyProgram program = new MyProgram(lines, null);
            MyProgram program1 = new MyProgram(lines, program);
            program.other = program1;
            program.OverrideRegister("p", 0);
            program1.OverrideRegister("p", 1);          
            while (!program.isWaiting || !program1.isWaiting)
            {
                program.Run();
                program1.Run();
            }
            Console.WriteLine("Part 2 solution: " + Environment.NewLine + program1.Sent);

        }
    }
    public class Register
    {
        public string Name { get; private set; }
        public long Value = 0;
        public Register(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return Name + ":" + Value;
        }
    }
    public class CPU
    {
        public List<Register> Registers = new List<Register>() { new Register("a"), new Register("b"), new Register("c"), new Register("d"), };
        public Register GetRegister(string name)
        {
            foreach (Register register in Registers)
            {
                if (name == register.Name)
                    return register;
            }
            Register r = new Register(name);
            Registers.Add(r);
            return r;
        }
    }
    public class MyProgram
    {
        public MyProgram other;
        string[] lines;
        int Driver = 0;
        public string Result = "";
        public long Sent = 0;
        public bool isWaiting = false;
        bool isTest = false;
        long FirstRec = 0;
        Queue<long> Q = new Queue<long>();
        CPU CPU = new CPU();
        public MyProgram(string[] lines)
        {
            this.lines = lines;
            isTest = true;
        }
        public MyProgram(string[] lines, MyProgram other)
        {
            this.lines = lines;
            this.other = other;
        }
        public void OverrideRegister(string register, int value)
        {
            CPU.GetRegister(register).Value = value;
        }
        public void Run()
        {
            while (Driver < lines.Length)
            {
                Exec(lines[Driver]);
                if (isWaiting)
                    break;
                Driver++;
            }
        }
        void Exec(string line)
        {
            string command = line.Split(' ')[0];
            switch (command)
            {
                case "set":
                    ExecCopy(line);
                    break;
                case "jgz":
                    JumpGreaterZero(line);
                    break;
                case "add":
                    Add(line);
                    break;
                case "mul":
                    Multiply(line);
                    break;
                case "mod":
                    Mod(line);
                    break;
                case "snd":
                    Out(line);
                    break;
                case "rcv":
                    Receive(line);
                    break;
            }
        }
        void Receive(string line)
        {
            if (isTest)
            {
                Console.WriteLine("Part 1 solution:");
                Console.WriteLine(FirstRec);
                isWaiting = true;
                return;
            }
            if(Q.Count == 0)
            {
                isWaiting = true;
                return;
            }
            CPU.GetRegister(line.Split(' ')[1]).Value = Q.Dequeue();
            
        }
        void Out(string line)
        {
            if (isTest)
            {
                FirstRec = CPU.GetRegister(line.Split(' ')[1]).Value;
                return;
            }
                
            Sent++;
            if(int.TryParse(line.Split(' ')[1], out int value))
                other.Q.Enqueue(value);
            else
                other.Q.Enqueue(CPU.GetRegister(line.Split(' ')[1]).Value);
            other.isWaiting = false;
        }
        void ExecCopy(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int valuetocopy))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value = valuetocopy;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value = CPU.GetRegister(line.Split(' ')[2]).Value;
        }
        void JumpGreaterZero(string line)
        {
            if (int.TryParse(line.Split(' ')[1], out int value))
            {
                if (value > 0)
                {
                    if (int.TryParse(line.Split(' ')[2], out int toskip))
                    {
                        Driver += toskip - 1;
                    }
                    else
                        Driver += (int)CPU.GetRegister(line.Split(' ')[2]).Value - 1;
                }
            }
            else
            {
                if (CPU.GetRegister(line.Split(' ')[1]).Value > 0)
                {
                    if (int.TryParse(line.Split(' ')[2], out int toskip))
                    {
                        Driver += toskip - 1;
                    }
                    else
                        Driver += (int)CPU.GetRegister(line.Split(' ')[2]).Value - 1;
                }
            }
        }
        void Add(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int value))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value += value;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value += CPU.GetRegister(line.Split(' ')[2]).Value;


        }
        void Multiply(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int value))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value *= value;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value *= CPU.GetRegister(line.Split(' ')[2]).Value;

        }
        void Mod(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int value))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value %= value;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value %= CPU.GetRegister(line.Split(' ')[2]).Value;
        }
    }
}