using System.Text;

namespace PPPP2_Sharp
{
    public class Answer
    {
        public int AnswerType;
        public double? First;
        public double? Second;

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append(AnswerType);
            if (First.HasValue)
            {
                sb.Append($" {First}");
            }

            if (Second.HasValue)
            {
                sb.Append($" {Second}");
            }

            return sb.ToString();
        }
    }

    public static class Solver
    {
        public static Answer SolveSystem(double a, double b, double c, double d, double e, double f)
        {
            Answer ans = new();


            if ((a == 0) && (b == 0) && (c == 0) && (d == 0) && (e == 0) && (f == 0))
            {
                ans.AnswerType = 5;
            }
            else if ((a * d - c * b != 0) && ((e * d - b * f != 0) || (a * f - c * e != 0)))
            {
                double y = (a * f - c * e) / (a * d - c * b);
                double x = (d * e - b * f) / (d * a - b * c);
                ans.AnswerType = 2;
                ans.First = x;
                ans.Second = y;
            }
            else if (((a * d - c * b == 0) && ((e * d - b * f != 0) || (a * f - c * e != 0))) ||
                     (a == 0 && c == 0 && e / b != f / d) ||
                     (b == 0 && d == 0 && e / a != f / c) ||
                     (a == 0 && b == 0 && c == 0 && d == 0 && (e / f > 0)))
            {
                if (((a == 0 && b == 0 && e == 0 && d != 0 && c == 0) ||
                     (c == 0 && d == 0 && f == 0 && b != 0 && a == 0)))
                {
                    double y = 0;
                    if (b == 0)
                        y = f / d;
                    else if (d == 0)
                        y = e / b;
                    else if (e == 0 || f == 0) // looks this branch is unreachable
                        y = 0;

                        

                    ans.AnswerType = 4;
                    ans.First = y;
                }
                else if (a == 0 && b == 0 && e == 0 && c != 0 && d == 0 ||
                         c == 0 && d == 0 && f == 0 && a != 0 && b == 0)
                {
                    double x = 0;
                    if (a == 0)
                        x = f / c;
                    else if (c == 0)
                        x = e / a;
                    else if (e == 0 || f == 0) // looks this branch is unreachable
                        x = 0;
  
                        

                    ans.AnswerType = 3;
                    ans.First = x;
                }
                else
                    ans.AnswerType = 0;
            }
            else if (a == 0 && c == 0)
            {
                double y;
                if (e == 0)
                    y = f / d;
                else if (f == 0) // well it's useless
                    y = e / b;
                else 
                    y = e / b;

                ans.AnswerType = 4;
                ans.First = y;
            }
            else if (b == 0 && d == 0)
            {
                double x;
                if (e == 0)
                {
                    x = f / c;
                }
                else if (f == 0) // well it's useless
                    x = e / a;
                else 
                    x = e / a;

                ans.AnswerType = 3;
                ans.First = x;
            }
            else if (b == 0 && e == 0)
            {
                double k, n;
                k = -c / d;
                n = f / d;
                ans.AnswerType = 1;
                ans.First = k;
                ans.Second = n;
            }
            else if (d == 0 && f == 0)
            {
                double k, n;
                k = -a / b;
                n = e / b;
                ans.AnswerType = 1;
                ans.First = k;
                ans.Second = n;
            }
            else if (a == 0 && e == 0)
            {
                double k, n;
                k = -d / c;
                n = f / c;
                ans.AnswerType = 1;
                ans.First = k;
                ans.Second = n;
            }
            else if (c == 0 && f == 0)
            {
                double k, n;
                k = -b / a;
                n = e / a;
                ans.AnswerType = 1;
                ans.First = k;
                ans.Second = n;
            }
            else if (a / b == c / d)
            {
                double k, n;
                k = -c / d;
                n = f / d;
                ans.AnswerType = 1;
                ans.First = k;
                ans.Second = n;
            }
            else
            {
                throw new ApplicationException("Wrong program logic");
            }

            return ans;
        }
    }

    public static class Program
    {
        private static List<double> ParseString(string str)
        {
            return str.Split(' ').Select(Convert.ToDouble).ToList();
        }

        private static void Main()
        {
            var nums = ParseString(Console.ReadLine());
            var res = Solver.SolveSystem(nums[0], nums[1], nums[2], nums[3], nums[4], nums[5]);
            Console.WriteLine(res);
        }
    }
}