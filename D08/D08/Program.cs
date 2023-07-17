using System.ComponentModel.Design;

namespace D08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CPU cpu = new CPU();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    cpu.ExecCommand(sr.ReadLine()!);
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(cpu.MaxRegVal());
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(cpu.MaxRegValAll());
        }
    }
    public class CPU
    {
        public List<Register> registers = new List<Register>();
        public Register GetRegister(string name) 
        {
            foreach (Register reg in registers)
            {
                if (reg.Name == name)
                {
                    return reg;
                }
            }
            Register toreturn = new Register(0, name);
            registers.Add(toreturn);
            return toreturn;
        }
        int maxval = int.MinValue;
        public int MaxRegVal()
        {
            registers.Sort((x1, x2) => (x1.Value.CompareTo(x2.Value) * -1));
            return registers.First().Value;
        }
        public int MaxRegValAll()
        {
            return maxval;
        }
        public void ExecCommand(string line)
        {
            if (CheckCondition(line.Split("if")[1].Trim()))
            {
                Register target = GetRegister(line.Split(' ')[0]);
                target.Value += (line.Split(' ')[1] == "inc" ? 1 : -1) * int.Parse(line.Split(' ')[2]);
                if(target.Value > maxval)
                    maxval = target.Value;
            }                         
        }
        bool CheckCondition(string line) 
        {
            Register tocheck = GetRegister(line.Split(' ')[0]);
            string op = line.Split(' ')[1];
            int with = int.Parse(line.Split(' ')[2]);
            switch (op)
            {
                case "<":
                    return tocheck.Value < with;
                case ">":
                    return tocheck.Value > with;
                case "==":
                    return tocheck.Value == with;
                case "!=":
                    return tocheck.Value != with;
                case ">=":
                    return tocheck.Value >= with;
                case "<=":
                    return tocheck.Value <= with;

            }
            throw new InvalidDataException();
        }
    }
    public class Register
    {
        public int Value;
        public string Name;
        public Register(int Value, string Name)
        {
            this.Value = Value;
            this.Name = Name;
        }
    }
}