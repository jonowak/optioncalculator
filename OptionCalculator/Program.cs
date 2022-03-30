using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionCalculator
{
    class BlackScholes
    {
        static void Main(string[] args)
        {

            Option call1 = new Option(92.0, 90.0, 0.25, true);

            int optionsHeld = 1000;
            //double spotDrift = 0.08;
            double sigma = 0.2;
            double interestRate = 0.05;
            double divRate = 0;
            double portfolio = 0;
            /*call1.CalculateOption(sigma, divRate, interestRate);
            call1.print();*/
            double hedgeShares;// = call1.DeltaHedge(divRate, optionsHeld);
            /*   Console.WriteLine("delta1 = {0}", call1.Delta.ToString());
               Console.WriteLine(hedgeShares.ToString());


               //example 
               //select * from option_price where securityId = 137410 and date = '10/3/2013' and callput = 'C' and strike = 22000 and expiration = '11/20/2013'
               //
               Option call2 = new Option(24.54, 22.0, 0.128767, true);
               sigma = 0.4007059;
               interestRate = 0.00228279788347442;
               divRate = 0;

               call2.CalculateOption(sigma, divRate, interestRate);
               call2.print();

               //example from IvyDB  
               //select * from option_price where securityId = 137410 and date = '10/3/2013' and callput = 'P' and strike = 22000 and expiration = '11/20/2013'
               //
               Option put = new Option(24.54, 22.0, 0.128767, false);
               sigma = 0.710466;
               interestRate = 0.00228279788347442;
               divRate = 0;
               put.CalculateOption(sigma, divRate, interestRate);
               put.print();*/

            //Problem #8
            Console.WriteLine("-----------------------------Problem 8---------------------------------------");
            Option put1 = new Option(100, 100, 0.5, false);
            optionsHeld = 1000;
            sigma = 0.30;
            interestRate = 0.05;
            divRate = 0;
            double cash = 0;
            int nShares = 0;

            put1.CalculateOption(sigma, divRate, interestRate);
            put1.print();
            hedgeShares = put1.DeltaHedge(divRate, optionsHeld);
            portfolio = optionsHeld * put1.Price + Math.Round(hedgeShares) * put1.Spot;
            Console.WriteLine("Day 1:");
            Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
            Console.WriteLine("Borrow and sell {0} shares of the asset. In other words, short sell {0} ", Math.Round(hedgeShares));
            Console.WriteLine("Portfolio value = {0}", portfolio);


            //problem 8 (ii)
            Console.WriteLine("Next trading day");
            double t = put1.Maturity - (1.0 / 252.0);
            Option put2 = new Option(102, 100, t, false);
            optionsHeld = 1000;
            sigma = 0.30;
            interestRate = 0.05;
            divRate = 0;

            put2.CalculateOption(sigma, divRate, interestRate);
            put2.print();
            //hedgeShares = put2.DeltaHedge(divRate, optionsHeld);
            double noHedge = (optionsHeld * put1.Price) - (optionsHeld * put2.Price);
            double portfolio2 = optionsHeld * put2.Price + Math.Round(hedgeShares) * put2.Spot;
            double loss = (optionsHeld * put2.Price + Math.Round(hedgeShares) * put2.Spot) - (optionsHeld * put1.Price + Math.Round(hedgeShares) * put1.Spot);
            Console.WriteLine("Day 1:");
            Console.WriteLine("Value of portfolio is {0}", portfolio2);
            Console.WriteLine("You lost {0}. Had you not hedged you would have lost {1}.", loss, noHedge);

            //book example
            Console.WriteLine("------------------------------Book example 27--------------------------------------");

            int optionsHeld25 = 1000;
            nShares = 400;
            sigma = 0.30;
            interestRate = 0.04;
            divRate = 0.0;
            cash = 10000;
            t = 0.5;
            double spot = 20.00;
            Option put25 = new Option(spot, 25.0, t, false);
            //How much is the portfolio worth? 

            put25.CalculateOption(sigma, divRate, interestRate);
            put25.print();
            portfolio = put25.Price * optionsHeld25 + put25.Spot * nShares + cash;
            Console.WriteLine("Day 1:");
            Console.WriteLine("Starting value of the portfolio is {0}", portfolio);

            hedgeShares = put25.DeltaHedge(divRate, optionsHeld);
            int buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;

            //new cash after share purchase 
            cash = cash - buyShares * put25.Spot; 
            Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
            Console.WriteLine("To obtain a delta neutral portfolio purchase {0} of the asset. The total cost of the purchase is {1}", buyShares, buyShares * put25.Spot);
            portfolio2 = optionsHeld25 * put25.Price + spot * nShares + cash - (buyShares * spot);
            Console.WriteLine("Portfolio value after hedging = {0}", portfolio2);
            //new shares after purchase
            nShares = nShares + buyShares;
   
            t = 5.00 / 12.00;
            put25.Price = 2.1818;
            spot = 24;
            cash = AccrueInterest(interestRate, cash);

            double portfolio3 = optionsHeld25 * put25.Price + spot * nShares + cash;
            Console.WriteLine("Portfolio value a month later = {0}", portfolio3);


            // problem 10 
            //long 1000 put options 
            //long 400 shares 
            Console.WriteLine("-----------------------------Problem 10---------------------------------------");
            Console.WriteLine();

              int nOptions = 1000;
              double strike = 30.00;
              nShares = 400;
              sigma = 0.30;
              spot = 25.00;
              interestRate = 0.02;
              divRate = 0.0;
              cash = 10000;
              t = 0.25;
              Option put30 = new Option(spot, strike, t, false);
              //How much is the portfolio worth? 
              

              put30.CalculateOption(sigma, divRate, interestRate);
              put30.print();
              Console.WriteLine("-----------W0BH------- S=30");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);

              portfolio = put30.Price * nOptions +  put30.Spot * nShares + cash;
              Console.WriteLine("Day 1:");
              Console.WriteLine("Starting value of the portfolio is {0}", portfolio);
            
              hedgeShares = put30.DeltaHedge(divRate, optionsHeld);
              buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;
              //new cash after share purchase 

              Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
              Console.WriteLine("Buy shares: {0}", buyShares);

              /*Console.WriteLine("To obtain a delta neutral portfolio purchase {0} of the asset. The total cost of the purchase is {1}", buyShares, buyShares * put30.Spot);
              //portfolio2 = optionsHeld25 * put25.Price + spot * nShares + cash - (buyShares * spot);
              portfolio2 = nOptions * put30.Price + spot * nShares + cash - (buyShares * spot);
              Console.WriteLine("Portfolio value after hedging = {0}", portfolio2);*/

              //new shares after purchase
              nShares = nShares + Math.Abs(buyShares);
              cash = cash - buyShares * put30.Spot;
              Console.WriteLine();
              Console.WriteLine("---------------W0 AH");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);
            // 1 month later
              Console.WriteLine("1 week later");
   
              cash = AccrueInterest(interestRate, cash);
              t = t - (1.0 / 52.0);
              spot = 30.00;
              put30 = null;
              put30 = new Option(spot, strike, t, false);
              put30.CalculateOption(sigma, divRate, interestRate);
              put30.print();

              Console.WriteLine();
              Console.WriteLine("--------------W1 BH--------S=30------");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);
              
            
              hedgeShares = put30.DeltaHedge(divRate, optionsHeld);
              buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;

              Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
              Console.WriteLine("Buy shares: {0}", buyShares);

              nShares = nShares + buyShares;
              cash = cash - buyShares * put30.Spot;
              Console.WriteLine("--------------W1 AH");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);

                

              Console.WriteLine();
              Console.WriteLine("2 weeks later");

              cash = AccrueInterest(interestRate, cash);
              t = t - (1.0 / 52.0);
              spot = 26.00;
              put30 = null;
              put30 = new Option(spot, strike, t, false);
              put30.CalculateOption(sigma, divRate, interestRate);
              put30.print();
              Console.WriteLine("--------------W2 BH------S=26---------");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);


              hedgeShares = put30.DeltaHedge(divRate, optionsHeld);
              buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;

              Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
              Console.WriteLine("Buy shares: {0}", buyShares);

              nShares = nShares + buyShares;
              cash = cash - buyShares * put30.Spot;
              Console.WriteLine("--------------W2 AH");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);

              Console.WriteLine();
              Console.WriteLine("3 weeks later");

              cash = AccrueInterest(interestRate, cash);
              t = t - (1.0 / 52.0);
              spot = 22.00;
              put30 = null;
              put30 = new Option(spot, strike, t, false);
              put30.CalculateOption(sigma, divRate, interestRate);
              put30.print();
              Console.WriteLine("--------------W3 BH---------S = 22");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);


              hedgeShares = put30.DeltaHedge(divRate, optionsHeld);
              buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;

              Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
              Console.WriteLine("Buy shares: {0}", buyShares);

              nShares = nShares + buyShares;
              cash = cash - buyShares * put30.Spot;
              Console.WriteLine("--------------W3 AH");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);

              Console.WriteLine();

              Console.WriteLine();
              Console.WriteLine("4 weeks later");

              cash = AccrueInterest(interestRate, cash);
              t = t - (1.0 / 52.0);
              spot = 27.00;
              put30 = null;
              put30 = new Option(spot, strike, t, false);
              put30.CalculateOption(sigma, divRate, interestRate);
              put30.print();
              Console.WriteLine("--------------W4 BH---------");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);


              hedgeShares = put30.DeltaHedge(divRate, optionsHeld);
              buyShares = Convert.ToInt32(Math.Round(hedgeShares)) - nShares;

              Console.WriteLine("Value of 1000 put hedge, Day 1: {0}", hedgeShares);
              Console.WriteLine("Buy shares: {0}", buyShares);

              nShares = nShares + buyShares;
              cash = cash - buyShares * put30.Spot;
              Console.WriteLine("--------------W4 AH");
              PrintPosition(nOptions * put30.Price, nShares * put30.Spot, cash);

              Console.WriteLine();


             
            Console.WriteLine();


            //---------- plot a function


        }//main

        public static double AccrueInterest(double r, double c)
        {
            double cash =  c * Math.Exp(r / 12.0);
            Console.WriteLine("Cash : {0}", cash.ToString());
            return cash;
        }

        public static void PrintPosition(double oP, double aP, double cP){
            Console.WriteLine("Option Position: {0}", oP.ToString());
            Console.WriteLine("Asset Position: {0}", aP.ToString());
            Console.WriteLine("Cash Position: {0}", cP.ToString());
            
        }
    }
}
