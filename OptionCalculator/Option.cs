using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionCalculator
{
    class Option
    {
        const double tolerance = 10.0e-12;
        const int n = 4; 
        //CUM_DIST_NORMAL constants
        const double a1 = 0.319381530;
        const double a2 = -0.356563782;
        const double a3 = 1.781477937;
        const double a4 = -1.821255978;
        const double a5 = 1.330274429;
        const double a = 0.2316419;

        private double strike;
        private double spot;
        private double maturity;
        private bool call;
        private double d1;
        private double d2;
        private double delta;
        private double price;
        private double gamma;
        private double vega;

        public double Vega
        {
            get { return vega; }
            set { vega = value; }
        }

        public double Gamma
        {
            get { return gamma; }
            set { gamma = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        } 


        public double Delta
        {
            get { return delta; }
            set { delta = value; }
        }

        public double D2
        {
            get { return d2; }
            set { d2 = value; }
        }

        public double D1
        {
            get { return d1; }
            set { d1 = value; }
        }


        public bool Call
        {
            get { return call; }
            set { call = value; }
        }

        public double Maturity
        {
            get { return maturity; }
            set { maturity = value; }
        }

        public double Spot
        {
            get { return spot; }
            set { spot = value; }
        }

        public double Strike
        {
            get { return strike; }
            set { strike = value; }
        }


        public Option()
        {
            spot = 0;
            strike = 0;
            maturity = 1.0;
            call = true;
            d1 = 0;
            d2 = 0;
            delta = 0;
            price = 0;
            gamma = 0;
            vega = 0;
        }

        public Option(double s, double k, double t, bool c)
        {
            Maturity = t;
            Spot = s;
            Strike = k;
            Call = c;
            d1 = 0;
            d2 = 0;
            delta = 0;
            price = 0;
            gamma = 0;
            vega = 0;
        }

        public void CalculateOption(double sigma, double q, double r)
        {
            SetDelta(sigma, q, r);
            GetOptionPrice(r, q);
            GetGamma(q, sigma);
            GetVega(q, sigma);

        }

        public void SetDelta(double sigma, double q, double r)
        {
            //N(d1)

            this.D1 = (Math.Log(Spot / Strike) + (r - q + (sigma * sigma) / 2.0) * Maturity) / (sigma * Math.Sqrt(Maturity));
            this.D2 = this.D1 - sigma*Math.Sqrt(Maturity);
            //Console.WriteLine("d1 = {0}", d1.ToString()); 
            //cummulative distribution of normal variable depanding on whether it's a call or a put
            
            double nOfD1 = CummulativeDistributionOfNormalVariable(this.D1);
            //Console.WriteLine("Delta calculated using psudo code 3.1: {0}", nOfD1.ToString());
            if (this.call)
                this.Delta = Math.Exp(-q * Maturity) * nOfD1;
            else
                this.Delta = -Math.Exp(-q * Maturity) * (1-nOfD1);
        }

        public double GetNOfD2()
        {   
            return (CummulativeDistributionOfNormalVariable(this.D2));
        }

        /*  private double DeltaCalculator(double sigma, double q, double r) 
          {
              double d1 = (Math.Log(Spot / Strike) + (r - q + (sigma * sigma) / 2.0) * Maturity) / (sigma * Math.Sqrt(Maturity));
              Console.WriteLine("d1 = {0}", d1.ToString());
              double nOfd1 = NumericalIntegration.ConvergenceMachine(NDistFunc, tolerance, 0.0, d1,  n);
              nOfd1 = nOfd1 / Math.Sqrt(2 * Math.PI);
              Console.WriteLine("N(d1) = {0}", nOfd1.ToString());
              Console.WriteLine("Delta calculated using numerical integration: {0}", delta);

              return nOfd1;
          }*/

        public double DeltaHedge(double q, double n)
        {
            double hedge;
            if (Call)
            {
                hedge = n * Math.Exp(-q * Maturity) * this.Delta;
            }
            else
            {
                hedge = -n * Math.Exp(-q * Maturity) * this.Delta;
            }
            return hedge;
        }

        private double CummulativeDistributionOfNormalVariable(double x) 
        {
            double z = Math.Abs(x);
            double y = 1.0/(1.0+a*z);
            double iSum = 1.0 - (Math.Exp(-(x * x) / 2.0)*(a1*y +a2*y*y + a3*y*y*y + a4*y*y*y*y + a5*y*y*y*y*y)/Math.Sqrt(2*Math.PI));

            if (x > 0)
                return iSum;
            else
                return 1.0 - iSum;

        }

        private static double NDistFunc(double x)
        {
            double r = (double)Math.Exp(-(x * x) * 0.5);//  also /2
            return r;
        }

        public double GetOptionPrice(double r, double q) 
        {
            //double nOfD2 = CummulativeDistributionOfNormalVariable(this.D2);
            if (this.Call)
            {
                this.price = (Spot * Math.Exp(-q * Maturity)) * this.Delta - (Strike * Math.Exp(-r * Maturity)) * CummulativeDistributionOfNormalVariable(this.D2);
                return price;
            }
            else
                this.price=  (Strike * Math.Exp(-r * Maturity)) * CummulativeDistributionOfNormalVariable(-(this.D2)) - (Spot * Math.Exp(-q * Maturity)) * CummulativeDistributionOfNormalVariable(-(this.D1));
            return price;

        }

        public double GetGamma(double q, double sigma){
            this.gamma = (Math.Exp(-q * Maturity) / (Spot * sigma * Math.Sqrt(Maturity))) * (1.0 / (Math.Sqrt(2 * Math.PI))) * Math.Exp(-D1 * D1 / 2);
            return gamma; 
        }

        public double GetVega(double q, double sigma)
        {

            this.vega = (Spot * Math.Exp(-q * Maturity)) * Math.Sqrt(Maturity) * (1.0 / (Math.Sqrt(2 * Math.PI))) * Math.Exp(-D1 * D1 / 2);
            return vega;
        }

        public void print() 
        {
            Console.WriteLine();
            if (this.Call) 
                Console.WriteLine("European Call") ;
            else 
                Console.WriteLine("European Put"); 

            Console.WriteLine("S ={0}, K = {1}, t = {2}, Delta = {3}, Gamma = {4}, Vega = {5}, Price = {6} ", this.Spot, this.Strike, this.Maturity, this.Delta, this.gamma, this.vega, this.price );
        }



    }
}
