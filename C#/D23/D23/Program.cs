namespace D23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");

            MyProgram program = new MyProgram(lines);
            program.Run(false);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(program.Sent);

            MyProgram program2 = new MyProgram(lines);
            program2.OverrideRegister("a", 1);
            program2.Run(true);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(program2.Result);
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
        public List<Register> Registers = new List<Register>() { new Register("a"), new Register("b"), new Register("c"), new Register("d"), new Register("e"), new Register("f"), new Register("g"), new Register("h"), };
        public Register GetRegister(string name)
        {
            foreach (Register register in Registers)
            {
                if (name == register.Name)
                    return register;
            }
            //Register r = new Register(name);
            //Registers.Add(r);
            //return r;
            throw new Exception("reg not found");
        }
    }
    public class MyProgram
    {
        string[] lines;
        int Driver = 0;
        public long Result { get { return CPU.GetRegister("h").Value; } }
        public long Sent = 0;
        CPU CPU = new CPU();
        public MyProgram(string[] lines)
        {
            this.lines = lines;
        }
        public void OverrideRegister(string register, int value)
        {
            CPU.GetRegister(register).Value = value;
        }
        public void Run(bool optimized)
        {
            while (Driver < lines.Length)
            {
                if(Driver > 7)
                {
                    if (optimized)
                    {
                        // well this took a while, so until b register reacher the c value, we take d and e registers starting from 2, incremending from e until b to check if there is a multiple e * d = b, that sets f to 0
                        // when e reaches b, we continue by incrementing d and resetting e to 2 ( this doesn't reset an already set f )
                        // when d reaches b, we check if f was set to 0, incrementing h, then the whole procedure increments b by 17 and resets everything to the start, so basically this is a reverse primality test, h doesn't get incremented if b is prime
                        // h gets incremented every time a non prime value is in b, and b has a start value and program ends when b = c... well this was fun to figure out
                        int count = 0;
                        for(long b = CPU.GetRegister("b").Value; b <= CPU.GetRegister("c").Value; b += 17) // im not sure if the += 17 is consistent across all inputs or not, it's the incrementing value of b from the penultimate line, you might want to change this
                        {
                            for(long div = 2; div < b / 2; div++)
                            {
                                if(b % div == 0)
                                {
                                    count++;
                                    break;
                                }
                                    
                            }                       
                        }
                        CPU.GetRegister("h").Value = count;
                        break;
                    }
                }
                Exec(lines[Driver]);
                Driver++;
            }
        }
        void Exec(string line)
        {
            string command = line.Split(' ')[0];
            switch (command)
            {
                case "set":
                    Set(line);
                    break;
                case "jnz":
                    JumpNotZero(line);
                    break;
                case "sub":
                    Decrease(line);
                    break;
                case "mul":
                    Multiply(line);
                    break;
            }
        }
        void Set(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int valuetocopy))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value = valuetocopy;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value = CPU.GetRegister(line.Split(' ')[2]).Value;
        }
        void JumpNotZero(string line)
        {
            if (int.TryParse(line.Split(' ')[1], out int value))
            {
                if (value != 0)
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
                if (CPU.GetRegister(line.Split(' ')[1]).Value != 0)
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
        void Decrease(string line)
        {
            if (int.TryParse(line.Split(' ')[2], out int value))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value -= value;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value -= CPU.GetRegister(line.Split(' ')[2]).Value;


        }
        void Multiply(string line)
        {
            Sent++;
            if (int.TryParse(line.Split(' ')[2], out int value))
            {
                CPU.GetRegister(line.Split(' ')[1]).Value *= value;
            }
            else
                CPU.GetRegister(line.Split(' ')[1]).Value *= CPU.GetRegister(line.Split(' ')[2]).Value;

        }
    }
}