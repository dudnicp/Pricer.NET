using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class HedgingPortfolio
    {
        private double riskyAsset;
        private double nonRiskyAsset;

        public double RiskyAsset
        {
            get { return riskyAsset; }
            set { riskyAsset = value; }
        }

        public double NonRiskyAsset
        {
            get { return nonRiskyAsset; }
            set { nonRiskyAsset = value; }
        }

        public HedgingPortfolio(double riskyAsset, double nonRiskyAsset)
        {
            this.riskyAsset = riskyAsset;
            this.nonRiskyAsset = nonRiskyAsset;
        }

        public HedgingPortfolio()
        {
            this.riskyAsset = 0;
            this.nonRiskyAsset = 0;
        }

        public HedgingPortfolio(HedgingPortfolio other)
        {
            this.riskyAsset = other.riskyAsset;
            this.nonRiskyAsset = other.nonRiskyAsset;
        }

    }
}
