using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetPricing.view
{
    class ConsoleView
    {
        private double riskyAsset = 0;
        private double nonRiskyAsset = 0;

        public double RiskyAsset
        {
            get
            {
                return riskyAsset;
            }

            set
            {
                riskyAsset = value;
            }
        }

        public double NonRiskyAsset
        {
            get
            {
                return nonRiskyAsset;
            }

            set
            {
                nonRiskyAsset = value;
            }
        }

        public void displayPortfolio(int period)
        {
            Console.WriteLine("--------------");
            Console.WriteLine("Portfolio Composition T" + period);
            Console.WriteLine("Risky Asset : " + riskyAsset);
            Console.WriteLine("Non Risky Asset : " + nonRiskyAsset);
        }

        public void pause()
        {
            Console.Read();
        }

        public void close()
        {
            // Nothing to do
        }

        public ConsoleView() { }
    }
}
