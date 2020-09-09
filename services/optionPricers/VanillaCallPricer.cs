using PricingApp.model;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingApp.utils;

namespace PricingApp.services.optionPricers
{
    public class VanillaCallPricer : OptionPricer
    {
        public VanillaCallPricer(VanillaCall opt) : base(opt)
        {
        }

        public override CompletePricingResults getPricingResults(List<DataFeed> pricingData)
        {
            double[,] underlyingSpots = ConvertDecimalArray.listDataFeedToDoubleArray(pricingData);
            double[] volatility = StatComputing.volatilitiesComputing(underlyingSpots);
            Pricer pricer = new Pricer();
            double spot = underlyingSpots[underlyingSpots.GetLength(0) - 1, 0];
            PricingResults pricingResults = pricer.Price((VanillaCall)opt, pricingData.Last().Date, 365, spot, volatility.First()); 
            CompletePricingResults completePricingResults = new CompletePricingResults(pricingResults, new double[] { spot });
            return completePricingResults;
        }

    }
}
