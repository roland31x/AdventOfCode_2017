namespace D02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int checksum1 = 0;
            int checksum2 = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream) 
                {
                    string[] tokens = sr.ReadLine()!.Replace('\t',' ').Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);
                    List<int> row = new List<int>();
                    foreach(string token in tokens)
                    {
                        row.Add(int.Parse(token));
                    }
                    row.Sort();
                    row.Reverse();
                    checksum1 += row.First() - row.Last();
                    for(int i = 0; i < row.Count; i++)
                    {
                        for(int j = i + 1; j < row.Count; j++)
                        {
                            if (row[i] % row[j] == 0)
                                checksum2 += row[i] / row[j];
                        }
                    }

                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(checksum1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(checksum2);
        }
    }
}