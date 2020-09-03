using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities;

namespace ProjetPricing.model
{
    class HedgingPortfolio
    {
        private VanillaCall opt;
        private const double r = 0.01;
        private double riskyAsset = 0;
        private double nonRiskyAsset = 0;
        private int nbPeriods = 0;
        private Pricer pricer = new Pricer();
        private double delta = 0;
        private int rebalancingPeriod;

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

        public int NbPeriods
        {
            get { return nbPeriods; }
            set { nbPeriods = value; }
        }

        public HedgingPortfolio(VanillaCall opt, DateTime startTime,
                                double spot, double sigma, double p, int rebalancingPeriod)
        {
            this.opt = opt;
            this.rebalancingPeriod = rebalancingPeriod;
            PricingResults pricingResult = pricer.Price(opt, startTime, 360, spot, sigma);
            delta = pricingResult.Deltas[0];
            this.riskyAsset = delta * spot;
            this.nonRiskyAsset = p - this.riskyAsset;
            this.nbPeriods = DayCount.CountBusinessDays(startTime, opt.Maturity) / rebalancingPeriod;
        }

        ~HedgingPortfolio()
        {
        }

        public void updateComposition(DateTime startTime, double spot, double sigma)
        {
            double oldDelta = this.delta;
            PricingResults pricingResult = pricer.Price(opt, startTime, 360, spot, sigma);
            delta = pricingResult.Deltas[0];
            this.riskyAsset = delta * spot;
            this.nonRiskyAsset = Math.Exp(r * rebalancingPeriod) + (oldDelta - delta) * spot;
        }
    }
}
