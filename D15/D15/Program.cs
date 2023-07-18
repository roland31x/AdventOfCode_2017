namespace D15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int GenA = 699;
            int GenB = 124; // input
            long AMultiplier = 16807;
            long BMultiplier = 48271;
            long Mod = 2147483647;
            int bitmask = 0;
            for(int i = 16; i < 31; i++)
            {
                bitmask += Pow(2, i);
            }

            int count = 0;
            for (int times = 0; times < 40000000; times++)
            {
                int ARes = (int)((AMultiplier * GenA) % Mod);
                int BRes = (int)((BMultiplier * GenB) % Mod);

                int BMask = BRes | bitmask;
                int AMask = ARes | bitmask;
                if (AMask == BMask)
                    count++;
                GenA = ARes;
                GenB = BRes;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(count);

            GenA = 699; // reset to initial input
            GenB = 124;
            count = 0;
            for (int times = 0; times < 5000000; times++)
            {
                int ARes = (int)((AMultiplier * GenA) % Mod);
                while (ARes % 4 != 0)
                {
                    GenA = ARes;
                    ARes = (int)((AMultiplier * GenA) % Mod);
                } 
                int BRes = (int)((BMultiplier * GenB) % Mod);
                while (BRes % 8 != 0)
                {
                    GenB = BRes;
                    BRes = (int)((BMultiplier * GenB) % Mod);
                }
                int BMask = BRes | bitmask;
                int AMask = ARes | bitmask;
                if (AMask == BMask)
                    count++;
                GenA = ARes;
                GenB = BRes;
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(count);
        }
        static int Pow(int nr, int pow)
        {
            if (pow == 0)
                return 1;
            else return nr * Pow(nr, pow - 1);
        }
    }
}