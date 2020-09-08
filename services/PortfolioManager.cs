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
        public List<TrackedResults> computePortfolioEvolution(DateTime testStart, DateTime testEnd,
                            int rebalancingPeriod, int estimationPeriod)
        {
            List<TrackedResults> res = new List<TrackedResults>();
            List<DataFeed> data = dataFeedProvider.GetDataFeed(optionPricer.Opt.UnderlyingShareIds, testStart,
                optionPricer.Opt.Maturity); // Données totales

            /* Calcul des portefeuilles sur toute la période */
            int i = 0;
            List<DataFeed> pricingData = data.GetRange(i * rebalancingPeriod, estimationPeriod);

            while (DateTime.Compare(pricingData.Last().Date, testEnd) < 0 && ((i+1) * rebalancingPeriod + estimationPeriod) < data.Count)
            {
                updateResults(pricingData, i);
                res.Add(new TrackedResults(results));
                i += 1;
                pricingData = data.GetRange(i * rebalancingPeriod, estimationPeriod);
            }
            return res;
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

            double riskyAsset = 0;
            for(int i = 0; i < spots.Length; i++)
            {
                riskyAsset += deltas[i] * spots[i];
            }

            if(periodIndex == 0)
            {
                results.PortfolioValue = optPrice;
            }
            else
            {
                results.PortfolioValue = results.Portfolio.RiskyAsset + results.Portfolio.NonRiskyAsset * Math.Exp(
                    r * (pricingData.Last().Date - pricingData.First().Date).Days / 365);
            }

            double nonRiskyAsset = results.PortfolioValue - riskyAsset;
            results.Portfolio = new HedgingPortfolio(riskyAsset, nonRiskyAsset);
            results.Payoff = optionPricer.Opt.GetPayoff(pricingData.Last().PriceList);
            results.TrackingError = (results.PortfolioValue - results.Payoff) / optPrice;
            results.Date = pricingData.Last().Date;

            deltas = newDeltas;
        }
    }
}
