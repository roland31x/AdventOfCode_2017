using System.Collections.Generic;

namespace D21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ExtendableMatrix> list = new List<ExtendableMatrix>();
            string[] read = File.ReadAllLines(@"..\..\..\input.txt");
            foreach(string line in read)
            {
                list.Add(new ExtendableMatrix(line));
            }
            Matrix initial = new Matrix(new int[,] { { 0, 1, 0 }, { 0, 0, 1 }, { 1, 1, 1 } });
            for(int i = 0; i < 5; i++)
            {
                ExtendableMatrix.Extend(ref initial, list);
                //initial.Show();
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(initial.Count());
            for (int i = 5; i < 18; i++)
            {
                ExtendableMatrix.Extend(ref initial, list);
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(initial.Count());
        }
    }
    public class Matrix
    {
        public int n { get { return mat.GetLength(0); } }
        public int m { get { return mat.GetLength(1); } }
        public int[,] mat;
        public Matrix(int[,] mat) 
        {
            this.mat = mat;
        }
        public void Show()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(mat[i, j]);
                }
                Console.WriteLine();
            }
        }
        public int Count()
        {
            int ret = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (mat[i, j] == 1)
                        ret++;
                }
            }
            return ret;
        }
        public bool IsSame(Matrix other) 
        {
            if (mat.GetLength(0) != other.mat.GetLength(0) || mat.GetLength(1) != other.mat.GetLength(1))
                return false;
            bool res = true;
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] != other.mat[i, j])
                        res = false;
                }
            }
            return res;
        }
        public Matrix Rotate(int times) // shamelessly stolen from geeks for geeks
        {
            if(times == 0)
                return Clone();
            // Consider all
            // squares one by one
            int[,] res = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    res[i, j] = mat[i, j];
                }
            }

            for(int k = 0; k < times; k++)
            {
                for (int x = 0; x < n / 2; x++)
                {
                    // Consider elements
                    // in group of 4 in
                    // current square
                    for (int y = x; y < n - x - 1; y++)
                    {
                        // store current cell
                        // in temp variable
                        int temp = res[x, y];

                        // move values from
                        // right to top
                        res[x, y] = res[y, n - 1 - x];

                        // move values from
                        // bottom to right
                        res[y, n - 1 - x] = res[n - 1 - x, n - 1 - y];

                        // move values from
                        // left to bottom
                        res[n - 1 - x, n - 1 - y] = res[n - 1 - y, x];

                        // assign temp to left
                        res[n - 1 - y, x] = temp;
                    }
                }
            }
            return new Matrix(res);


        }
        public Matrix Flip(int vertorhorz)
        {
            int[,] res = new int[n, m];
            if(vertorhorz == 0) // vertical flip
            {
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        res[i, j] = mat[i, m - 1 - j];
                    }
                }
            }
            else // horizontal
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        res[i, j] = mat[n - 1 - i, j];
                    }
                }
            }
            return new Matrix(res);
        }
        public Matrix Clone()
        {
            int[,] clone = new int[n, m];
            for(int i = 0; i < m; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    clone[i,j] = mat[i,j];
                }
            }
            return new Matrix(clone);
        }
    }
    public class ExtendableMatrix
    {
        public static void Extend(ref Matrix mat, List<ExtendableMatrix> list)
        {
            if(mat.n % 2 == 0)
            {
                Matrix[,] dissected = Dissect(mat, 2);
                Upscale(dissected, list);              
                mat = Stitch(dissected);

            }
            else
            {
                Matrix[,] dissected = Dissect(mat, 3);
                Upscale(dissected, list);
                mat = Stitch(dissected);
            }
        }
        static void Upscale(Matrix[,] dissected, List<ExtendableMatrix> list)
        {
            for (int i = 0; i < dissected.GetLength(0); i++)
            {
                for (int j = 0; j < dissected.GetLength(1); j++)
                {
                    bool ok = false;
                    foreach (ExtendableMatrix e in list)
                    {
                        if (dissected[i, j].IsSame(e.pattern))
                        {
                            dissected[i, j] = e.output.Clone();
                            ok = true;
                            break;
                        }
                    }
                    if (!ok)
                    {
                        foreach (ExtendableMatrix e in list)
                        {
                            for (int r = 0; r < 4; r++)
                            {
                                Matrix rotated = dissected[i, j].Rotate(r);
                                if (rotated.IsSame(e.pattern))
                                {
                                    dissected[i, j] = e.output.Clone();
                                    ok = true;
                                    break;
                                }
                                if (!ok)
                                {
                                    for (int f = 0; f < 2; f++)
                                    {
                                        Matrix flipped = rotated.Flip(f);
                                        if (flipped.IsSame(e.pattern))
                                        {
                                            dissected[i, j] = e.output.Clone();
                                            ok = true;
                                            break;
                                        }
                                    }
                                }
                                if (ok)
                                    break;
                            }
                            if (ok)
                                break;
                        }
                    }
                }
            }
        }
        static Matrix Stitch(Matrix[,] dissected)
        {
            int newlen = dissected[0, 0].n;
            int[,] result = new int[dissected.GetLength(0) * newlen, dissected.GetLength(1) * newlen];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = dissected[i / newlen, j / newlen].mat[i % newlen, j % newlen];
                }
            }
            return new Matrix(result);
        }
        static Matrix[,] Dissect(Matrix mat, int mod)
        {
            Matrix[,] dissected = new Matrix[mat.n / mod, mat.m / mod];
            for (int i = 0; i < mat.n / mod; i++)
            {
                for (int j = 0; j < mat.m / mod; j++)
                {
                    int[,] result = new int[mod, mod];
                    for (int k1 = i * mod; k1 < i * mod + mod; k1++)
                    {
                        for (int k2 = j * mod; k2 < j * mod + mod; k2++)
                        {
                            result[k1 % mod, k2 % mod] = mat.mat[k1, k2];
                        }
                    }
                    dissected[i, j] = new Matrix(result);
                }
            }
            return dissected;
        }

        public Matrix pattern;
        public Matrix output;
        public ExtendableMatrix(string line)
        {
            string first = line.Split("=>")[0].Trim();
            string last = line.Split("=>")[1].Trim();
            string[] firstmat = first.Split('/');
            int firstsize = firstmat.Length;
            int[,] pat = new int[firstsize, firstsize];
            for(int i = 0; i < firstmat.Length; i++)
            {
                for(int j = 0; j < firstmat[i].Length; j++)
                {
                    if (firstmat[i][j] == '#')
                        pat[i, j] = 1;
                }
            }
            pattern = new Matrix(pat);

            string[] secondmat = last.Split('/');
            int secondsize = secondmat.Length;
            int[,] output = new int[secondsize, secondsize];
            for (int i = 0; i < secondmat.Length; i++)
            {
                for (int j = 0; j < secondmat[i].Length; j++)
                {
                    if (secondmat[i][j] == '#')
                        output[i, j] = 1;
                }
            }
            this.output = new Matrix(output);
        }
    }
}