namespace D04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int ok1 = 0;
            int ok2 = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    bool good1 = true;
                    bool good2 = true;
                    string[] pass = sr.ReadLine()!.Split(' ');
                    for(int i = 0; i < pass.Length - 1; i++)
                    {
                        char[] anagram = pass[i].ToCharArray();
                        Array.Sort(anagram);
                        for(int j = i + 1; j < pass.Length; j++)
                        {
                            char[] tocheck = pass[j].ToCharArray();
                            Array.Sort(tocheck);
                            if (pass[i] == pass[j])
                            {
                                good1 = false; 
                            }
                            if(tocheck.Length == anagram.Length)
                            {
                                for(int k = 0; k < tocheck.Length; k++)
                                {
                                    if (tocheck[k] != anagram[k])
                                        break;
                                    if (k == tocheck.Length - 1)
                                        good2 = false;
                                }
                            }                                                     
                        }
                    }
                    if (good1)
                        ok1++;
                    if (good2)
                        ok2++;
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(ok1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(ok2);
        }

    }
}