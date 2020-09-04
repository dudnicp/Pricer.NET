using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetPricing.model;
using ProjetPricing.view;

namespace ProjetPricing.viewModel
{
    class PortfolioViewModel
    {
        private HedgingPortfolio portfolio;
        private ConsoleView view;

        public void displayPortfolioEvolution()
        {
            for (int i = 0; i < portfolio.NbPeriods; i++)
            {
                portfolio.updateComposition(new DateTime(), 31, 0.5);
                view.RiskyAsset = portfolio.RiskyAsset;
                view.NonRiskyAsset = portfolio.NonRiskyAsset;
                view.update();
                view.pause();
            }
            view.close();
        }

        public PortfolioViewModel(HedgingPortfolio portfolio, ConsoleView view)
        {
            this.portfolio = portfolio;
            this.view = view;
        }
    }
}
