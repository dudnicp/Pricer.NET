using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities;
using PricingApp.model;
using PricingApp.services.optionPricers;

namespace PricingApp.services
{
    public class PortfolioManager
    {
        IDataFeedProvider dataFeedProvider;
        private OptionPricer optionPricer;
        private TrackedResults results;
        private const double r = 0.01;
        private double[] deltas;

        public HedgingPortfolio Portfolio { get; }


        public PortfolioManager(OptionPricer optionPricer, IDataFeedProvider dataFeedProvider)
        {
            this.dataFeedProvider = dataFeedProvider;
            this.optionPricer = optionPricer;
            results = new TrackedResults();
            deltas = new double[optionPricer.Opt.UnderlyingShareIds.Length];
        }

        // Crée une liste de resultats (l'évolution du portefeuille de l'option sur la durée demandée)
        public void computePortfolioEvolution(DateTime testStart, DateTime testEnd,
                            int rebalancingPeriod, int estimationPeriod)
        {
            List<DataFeed> data = dataFeedProvider.GetDataFeed(optionPricer.Opt.UnderlyingShareIds, testStart,
                optionPricer.Opt.Maturity); // Données totales

            /* Calcul des portefeuilles sur toute la période */
            int i = 0;
            List<DataFeed> pricingData = data.GetRange(i * rebalancingPeriod, estimationPeriod);

            while (DateTime.Compare(pricingData.Last().Date, testEnd) < 0)
            {
                updateResults(pricingData, i);
                i += 1;
                pricingData = data.GetRange(i * rebalancingPeriod, estimationPeriod);
            }
        }

        private void updateResults(List<DataFeed> pricingData, int periodIndex)
        {

            CompletePricingResults pricingResult = optionPricer.getPricingResults(pricingData);
            double[] newDeltas = pricingResult.Deltas;
            double[] spots = pricingResult.Spots;
            double optPrice = pricingResult.Price;

            if(periodIndex == 0)
            {
                deltas = newDeltas;
            }

            double sum = 0;
            for(int i = 0; i < spots.Length; i++)
            {
                sum += deltas[i] * spots[i];
            }

            results.Portfolio.RiskyAsset = sum;

            if(periodIndex == 0)
            {
                results.PortfolioValue = optPrice;
            }
            else
            {
                results.PortfolioValue = results.Portfolio.RiskyAsset + results.Portfolio.NonRiskyAsset * Math.Exp(
                    r * DayCount.CountBusinessDays(pricingData.First().Date, pricingData.Last().Date) / 365);
            }

            results.Portfolio.NonRiskyAsset = results.PortfolioValue - results.Portfolio.RiskyAsset;
            results.Payoff = ((optPrice - optionPricer.Opt.Strike) > 0) ? (optPrice - optionPricer.Opt.Strike) : 0;
            results.TrackingError = (results.PortfolioValue - results.Payoff) / optPrice;

            deltas = newDeltas;
        }



        /*protected Dictionary<string, (double, double)> getVolatilityAndSpot(List<DataFeed> data)
        {
            Dictionary<string, (double, double)> sharesData = new Dictionary<string, (double, double)>();
            Dictionary<string, List<decimal>> shareSpots = new Dictionary<string, List<decimal>>();
            double vol, spot;
            foreach (DataFeed dataFeed in data)
            {
                foreach(KeyValuePair<string, decimal> kvp in dataFeed.PriceList)
                {
                    shareSpots[]
                }
            }
            spot = Decimal.ToDouble(values.Last());
            vol = ComputeSd(values);
            return (vol, spot);
        }

        protected double ComputeSd(List<decimal> values)
        {
            decimal moyenne = 0;
            double moy, var = 0;
            foreach (decimal val in values)
            {
                moyenne += val;
            }
            moy = Decimal.ToDouble(moyenne / values.Count);

            foreach(decimal val in values)
            {
                var += Math.Pow(moy - Decimal.ToDouble(val), 2);
            }
            var = Math.Sqrt(var);
            return var;
        }*/
    }
}
