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
            ConsoleView view = new ConsoleView();
            PortfolioViewModel viewModel = new PortfolioViewModel(view);

            viewModel.runApp();
        }
    }
}
