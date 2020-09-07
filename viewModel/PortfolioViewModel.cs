using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using PricingApp.model;
using PricingApp.services;

namespace PricingAPp.viewModel
{
    public class PortfolioViewModel
    {
        public void runApp()
        {
            Share share = new Share("ACCOR SA", "AC FP");
            VanillaCall opt = new VanillaCall("opt", share, new DateTime(2020, 12, 31), 20);
            DateTime testStart = new DateTime(2020, 1, 1);
            DateTime testEnd = new DateTime(2020, 10, 1);
            int rebalancingPeriod = 20;
            int estimationPeriod = 10;
            rebalancingPeriod = 20;
            estimationPeriod = 10;
        }
    }
}
