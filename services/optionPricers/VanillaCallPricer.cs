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
            int nShares = underlyingSpots.GetLength(1);
            double[] spots = new double[nShares - 1];
            for (int i = 0; i < nShares; i++)
            {
                spots[i] = underlyingSpots[nShares - 1, i];
            }
            PricingResults pricingResults = pricer.Price((VanillaCall)opt, pricingData.Last().Date, 365, spots.First(), volatility.First()); ;
            CompletePricingResults completePricingResults = new CompletePricingResults(pricingResults, spots);
            return completePricingResults;
        }
    }
}
