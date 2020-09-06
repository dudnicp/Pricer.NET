using PricingApp.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PricingApp.view
{
    public class ConsoleView : View
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

        public void update(Result res)
        {
            System.Diagnostics.Debug.WriteLine("--------------");
            System.Diagnostics.Debug.WriteLine("Risky Asset : " + res.Portfolio.RiskyAsset);
            System.Diagnostics.Debug.WriteLine("Non Risky Asset : " + res.Portfolio.NonRiskyAsset);
        }

        public ConsoleView() { }
    }
}
