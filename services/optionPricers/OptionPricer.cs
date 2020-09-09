using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingApp.model;

namespace PricingApp.services.optionPricers
{
    public abstract class OptionPricer
    {
        protected Option opt;

        public Option Opt { get { return opt; } }

        public OptionPricer(Option opt)
        {
            this.opt = opt;
        }
        public abstract CompletePricingResults getPricingResults(List<DataFeed> pricingData);

    }
}
