using System;

namespace gradientSearchAlgorithm
{
    class Program
    {
        static double[] gradient(double[] x)
        {
            double[] result = new double[2];
            result[0] = 4 + 2 * x[0] - 4 * Math.Pow(x[0], 3) - 2 * x[1];
            result[1] = 2 - 2 * x[0] - 2 * x[1];
            return result;
        }
        static double f_t(double[] x, double[] gra, double t)
        {
            //original function => 4 * x[0] + 2 * x[1] + Math.Pow(x[0], 2) - Math.Pow(x[0], 4) - 2 * x[0] * x[1] - Math.Pow(x[1], 2) 
            double P = 4 * (x[0] + gra[0] * t) + 2 * (x[1] + gra[1] * t) + Math.Pow((x[0] + gra[0] * t), 2) 
                - Math.Pow((x[0] + gra[0] * t), 4) - 2 * (x[0] + gra[0] * t) * (x[1] + gra[1] * t) - Math.Pow((x[1] + gra[1] * t), 2);
            return P;
        }
        static double derivative_t(double[] x, double[] gra, double t)
        {
            //türevin limit tanımı
            // 10^(-5) is used for very small number 
            double Pprime = (f_t(x, gra, t + Math.Pow(10, -5)) - f_t(x, gra, t)) * Math.Pow(10, 5);
            return Pprime;
        }
        static double s_derivative_t(double[] x, double[] gra, double t)
        {
            double Pprime_prime = (derivative_t(x, gra, t + Math.Pow(10, -5)) - derivative_t(x, gra, t)) * Math.Pow(10, 5);
            return Pprime_prime;
        }
        static void Main(string[] args)
        {
            double epsilon = 0.001;

            double[] x = new double[2];

            Console.Write("enter the first element of the initial point: ");
            x[0] = Convert.ToDouble(Console.ReadLine());
            Console.Write("\nenter the second element of the initial point: ");
            x[1] = Convert.ToDouble(Console.ReadLine());

            double[] gra = new double[2];
                
            int count = 0;
            
            //--------------------------------------newton----------------------------------------- 
            do
            {
                gra = gradient(x);
                double epsilon_t = 0.01;
                double t_new = 0;

                do
                {
                    if (derivative_t(x, gra, t_new) != 0)
                    {
                        t_new = t_new - derivative_t(x, gra, t_new) / s_derivative_t(x, gra, t_new);
                    }
                    else
                    {
                        break;
                    }
                }
                while (Math.Abs(derivative_t(x, gra, t_new)) > epsilon_t);
                
                Console.WriteLine("\n\nx[" + count + "] = [" + x[0] + "," + x[1] + "]");
                count++;

                x[0] = x[0] + gra[0] * t_new;
                x[1] = x[1] + gra[1] * t_new;

                gra = gradient(x);
            }
            while (Math.Sqrt(Math.Pow(gra[0], 2) + Math.Pow(gra[1], 2)) > epsilon); 
            /*
            //--------------------------------------bisection-----------------------------------------         
            do
            {
                gra = gradient(x);
                
                double t_bottom = 0;
                double t_top = 1;
                double epsilon_t = 0.01;

                double t_avg;
                do
                {
                    t_avg = (t_bottom + t_top) / 2;
                    if(derivative_t(x, gra, t_avg) > 0)
                    {
                        t_bottom = t_avg;
                    }
                    else if(derivative_t(x, gra, t_avg) < 0)
                    {
                        t_top = t_avg;
                    }
                    else
                    {
                        //t is found
                        break;
                    }
                }
                while (t_top - t_bottom > epsilon_t);
                
                Console.WriteLine("\n\nx[" + count + "] = [" + x[0] + "," + x[1] + "]");
                count++;

                x[0] = x[0] + gra[0] * t_avg;
                x[1] = x[1] + gra[1] * t_avg; 

                gra = gradient(x);
            }
            while (Math.Sqrt(Math.Pow(gra[0], 2) + Math.Pow(gra[1], 2)) > epsilon); */
            
            Console.ReadKey();
        }
    }
}
