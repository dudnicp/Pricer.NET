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
using PricingApp.services;

namespace PricingApp.services.optionPricers
{
    public class BasketOptionPricer : OptionPricer
    {

        public BasketOptionPricer(BasketOption opt) : base(opt)
        {
        }

        public override CompletePricingResults getPricingResults(List<DataFeed> pricingData)
        {
            double[,] underlyingSpots = ConvertDecimalArray.listDataFeedToDoubleArray(pricingData);
            double[,] myCorrelationMatrix = StatComputing.correlationMatrix(underlyingSpots);
            double[] volatility = StatComputing.volatilitiesComputing(underlyingSpots);
            Pricer pricer = new Pricer();
            int nShares = underlyingSpots.GetLength(1);
            double[] spots = new double[nShares];
            for(int i = 0; i < nShares; i++)
            {
                spots[i] = underlyingSpots[nShares - 1, i];
            }
            PricingResults pricingResults = pricer.Price((BasketOption)opt, pricingData.Last().Date, 365, spots, volatility, myCorrelationMatrix); ;            
            CompletePricingResults completePricingResults = new CompletePricingResults(pricingResults,spots);
            return completePricingResults;
        }
    }
}
