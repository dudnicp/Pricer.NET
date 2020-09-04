using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.FinancialProducts;
using ProjetPricing.model;
using ProjetPricing.view;
using ProjetPricing.viewModel;

namespace ProjetPricing
{
    class Program
    {
        public static void Main(string[] args)
        {
            Share share = new Share("share", "ID");
            VanillaCall opt = new VanillaCall("salut", share, new DateTime(2000, 12, 31), 100);
            HedgingPortfolio portfolio = new HedgingPortfolio(opt, new DateTime(2000, 1, 1), 31, 0.1, 50, 1);
            ConsoleView view = new ConsoleView();
            PortfolioViewModel viewModel = new PortfolioViewModel(portfolio, view);

            viewModel.displayPortfolioEvolution();
            Console.ReadLine();
        }
    }
}
