using System.Security.Cryptography.X509Certificates;

namespace D03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = 347991;
            Console.WriteLine(FindDist(input));
            Console.WriteLine(FindFirstBiggest(input));

        }
        static int FindFirstBiggest(int input) 
        {
            int m = 11;
            int[,] mat = new int[m, m];
            mat[m / 2, m / 2] = 1;
            int currI = m / 2;
            int currJ = m / 2;
            int currentlength = 3;
            while (mat[currI, currJ] < input) 
            {
                int i = currI;
                int j = currJ + 1;
                mat[i, j] += GetNeighbors(i, j, mat);
                if (mat[i,j] > input)
                    return mat[i, j];
                int times = currentlength - 2;
                while (times > 0)
                {
                    i--;
                    mat[i, j] += GetNeighbors(i, j, mat);
                    if (mat[i, j] > input)
                        return mat[i, j];
                    times--;
                }
                times = currentlength - 1;
                while (times > 0)
                {
                    j--;
                    mat[i, j] += GetNeighbors(i, j, mat);
                    if (mat[i, j] > input)
                        return mat[i, j];
                    times--;
                }
                times = currentlength - 1;
                while (times > 0)
                {
                    i++;
                    mat[i, j] += GetNeighbors(i, j, mat);
                    if (mat[i, j] > input)
                        return mat[i, j];
                    times--;
                }
                times = currentlength - 1;
                while (times > 0)
                {
                    j++;
                    mat[i, j] += GetNeighbors(i, j, mat);
                    if (mat[i, j] > input)
                        return mat[i, j];
                    times--;
                }
                currentlength+= 2;
                currI = i;
                currJ = j;
            }
            return mat[currI, currJ];
        }
        static int GetNeighbors(int i, int j, int[,] mat)
        {
            int sum = 0;
            for(int ix = i - 1; ix <= i + 1; ix++)
            {
                for(int jx = j - 1; jx <= j + 1; jx++)
                {
                    if (jx == j && ix == i)
                        continue;
                    sum += mat[ix, jx];
                }
            }
            return sum;
        }
        static int FindDist(int input)
        {
            int idx = 3;
            while (idx * idx < input)
            {
                idx += 2;
            }
            idx -= 2;
            int start = idx * idx + 1;
            int currentlength = idx + 2;
            int i = currentlength - 2;
            int j = currentlength - 1;
            while (i > 0 && start != input)
            {
                i--;
                start++;
            }
            while (j > 0 && start != input)
            {
                j--;
                start++;
            }
            while (start != input && i < currentlength - 1)
            {
                i++;
                start++;
            }
            while (start != input && j < currentlength - 1)
            {
                j++;
                start++;
            }
            int target = currentlength / 2;
            List<int> dist = new List<int>() { target - i, target - j, j - target, i - target };
            dist.Sort();
            return dist.Where(x => x >= 0).First() + currentlength / 2;
        }
    }
}