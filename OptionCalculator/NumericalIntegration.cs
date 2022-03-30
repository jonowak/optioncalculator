using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionCalculator
{
    class NumericalIntegration
    {
        private double x1;
        private double x2;
        private double n;
        private double tolerance;
        private double[] toleranceArray;
        private Func<double, double> func;

        //private double integralValue;

        public NumericalIntegration() 
        { 
            x1 = 0.0;
            x2 = 0.0;
            n = 4;
            tolerance = (double)10.0e-12;
        }
        public NumericalIntegration(double x1, double x2, double n, double tolerance, Func<double, double> f) 
        {
            this.x1 = x1;
            this.x2 = x2;
            this.n = n;
            this.tolerance = tolerance;
            this.func = f;
        }
        public NumericalIntegration(double x1, double x2, double n, double[] toleranceArray, Func<double, double> f) 
        {
            this.x1 = x1;
            this.x2 = x2;
            this.n = n;
            this.toleranceArray = toleranceArray;
            this.func = f;
        }

        public static double Integrate(Func<double, double> f, double x1, double x2, double n)
        {

            double h = (x2 - x1) / (double)n;
            double integrationSum;
            integrationSum = (f(x1) + f(x2)) / (double)6.0;

            for (int i = 1; i < n; i++)
                integrationSum += f(x1 + i * h) / (double)3.0;

            for (int i = 1; i <= (n); i++)
                integrationSum += (double)2.0 * f(x1 + ((double)i - (double)0.5) * h) / (double)3.0;

            integrationSum *= h;
            return integrationSum;

        }

        public static double ConvergenceMachine(Func<double, double> f, double t, double x1, double x2, double n)
        {
            double valPrev = Integrate(f, x1, x2, n);
            Console.WriteLine(n.ToString() + " " + Math.Round(valPrev, 12).ToString());
            n *= 2;
            double valCurrent = Integrate(f, x1, x2, n);
            Console.WriteLine(n.ToString() + " " + Math.Round(valCurrent, 12).ToString());

            while (Math.Abs(valCurrent - valPrev) > t)
            {
                valPrev = valCurrent;
                n *= 2;
                valCurrent = Integrate(f, x1, x2, n);
                Console.WriteLine(n.ToString() + "  " + Math.Round(valCurrent, 12).ToString());
            }

            return valCurrent;
        }

        static double NDistFunc(double x)
        {
            double r = (double)Math.Exp(-(x * x) * 0.5);//  also /2
            return r;
        }


        // get and set functions
        public double X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        public double X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        public double N
        {
            get { return n; }
            set { n = value; }
        }


        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public double[] ToleranceArray
        {
            get { return toleranceArray; }
            set { toleranceArray = value; }
        }

        public Func<double, double> Func
        {
            get { return func; }
            set { func = value; }
        }
    }
}
