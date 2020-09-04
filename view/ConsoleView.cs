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
        private double period = 0;

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

        public void update()
        {
            System.Diagnostics.Debug.WriteLine("--------------");
            System.Diagnostics.Debug.WriteLine("Portfolio Composition T" + period);
            System.Diagnostics.Debug.WriteLine("Risky Asset : " + riskyAsset);
            System.Diagnostics.Debug.WriteLine("Non Risky Asset : " + nonRiskyAsset);
            period++;
        }

        public void pause()
        {
        }

        public void close()
        {
            // Nothing to do
        }

        public ConsoleView() { }
    }
}
