using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using ProjetPricing.model;
using ProjetPricing.view;

namespace ProjetPricing.viewModel
{
    class PortfolioViewModel
    {
        private ConsoleView view;

        public void displayPortfolioEvolution(VanillaCall opt, DateTime testStart, DateTime testEnd,
            int pricingPeriod, int estimationPeriod, IDataFeedProvider dataFeedProvider)
        {
            PortfolioManager manager = new PortfolioManager(opt, dataFeedProvider);
            List<Result> results = manager.computePortfolioEvolution(testStart, testEnd, 
                                estimationPeriod, pricingPeriod);
            foreach (Result res in results)
            {
                view.RiskyAsset = res.Portfolio.RiskyAsset;
                view.NonRiskyAsset = res.Portfolio.NonRiskyAsset;
                view.update();
            }
        }

        public PortfolioViewModel(ConsoleView view)
        {
            this.view = view;
        }

        public void runApp()
        {
            Share share = new Share("ACCOR SA", "AC FP");
            VanillaCall opt = new VanillaCall("opt", share, new DateTime(2020, 31, 12), 100);
            DateTime testStart = new DateTime(2020, 1, 1);
            DateTime testEnd = new DateTime(2020, 10, 1);
            int pricingPeriod = 20;
            int estimationPeriod = 50;
            displayPortfolioEvolution(opt, testStart, testEnd, pricingPeriod, estimationPeriod,
                new SimulatedDataFeedProvider());
        }
    }
}
