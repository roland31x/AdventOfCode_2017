namespace D17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = 354; // input
            Spinlock spinlock = new Spinlock(input); 
            spinlock.Fill(2017);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(spinlock.GetNextVal());
            QuickSpinlock quickSpinlock = new QuickSpinlock(input);
            quickSpinlock.Fill(50000000);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(quickSpinlock.GetNextVal());

        }
    }
    public class QuickSpinlock
    {
        int next = -1;
        int driver = 0;
        int size = 1;
        int jump;
        public QuickSpinlock(int jumpamount)
        {
            jump = jumpamount;
        }
        public void Fill(int howmany)
        {
            while (size < howmany + 1)
            {
                driver += jump;
                driver %= size;
                if (driver + 1 == 1)
                    next = size;
                size++;
                driver++;
            }
        }
        public int GetNextVal()
        {
            return next;
        }
    }
    public class Spinlock
    {
        int[] buf = new int[3000];
        int driver = 0;
        int size = 1;
        int jump;
        public Spinlock(int jumpamount) 
        {
            jump = jumpamount;
        }
        public void Fill(int howmany)
        {
            while(size < howmany + 1)
            {
                driver += jump;
                driver %= size;
                for(int i = buf.Length - 2; i >= driver + 1; i--)
                {
                    (buf[i], buf[i + 1]) = (buf[i + 1], buf[i]);
                }
                buf[driver + 1] = size;
                size++;
                driver++;
            }
        }
        public int GetNextVal()
        {
            return buf[driver + 1];
        }
    }
}