using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using PricingApp.view;
using PricingApp.model;

namespace PricingAPp.viewModel
{
    public class PortfolioViewModel
    {
        private View view;

        public void displayPortfolioEvolution(VanillaCall opt, DateTime testStart, DateTime testEnd,
            int rebalancingPeriod, int estimationPeriod, IDataFeedProvider dataFeedProvider)
        {
            PortfolioManager manager = new PortfolioManager(opt, dataFeedProvider);
            List<Result> results = manager.computePortfolioEvolution(testStart, testEnd, 
                                rebalancingPeriod, estimationPeriod);
            foreach (Result res in results)
            {
                view.update(res);
            }
        }

        public PortfolioViewModel(View view)
        {
            this.view = view;
        }

        public void runApp()
        {
            Share share = new Share("ACCOR SA", "AC FP");
            VanillaCall opt = new VanillaCall("opt", share, new DateTime(2020, 12, 31), 100);
            DateTime testStart = new DateTime(2020, 1, 1);
            DateTime testEnd = new DateTime(2020, 10, 1);
            int rebalancingPeriod = 20;
            int estimationPeriod = 10;
            displayPortfolioEvolution(opt, testStart, testEnd, rebalancingPeriod, estimationPeriod,
                new SimulatedDataFeedProvider());
        }
    }
}
