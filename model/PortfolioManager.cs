using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities;

namespace ProjetPricing.model
{
    class PortfolioManager
    {
        IDataFeedProvider dataFeedProvider;
        private VanillaCall opt;
        private const double r = 0.01;

        public VanillaCall Opt { get { return opt; } }


        public PortfolioManager(VanillaCall opt, IDataFeedProvider dataFeedProvider)
        {
            this.dataFeedProvider = dataFeedProvider;
            this.opt = new VanillaCall(opt.Name, opt.UnderlyingShare, opt.Maturity, opt.Strike);
        }

        // Crée une liste de resultats (l'évolution du portefeuille de l'option sur la durée demandée)
        public List<Result> computePortfolioEvolution(DateTime testStart, DateTime testEnd,
                            int estimationPeriod, int rebalancingPeriod)
        {
            Pricer pricer = new Pricer(); // Création Pricer pour la suite de la fonction

            List<Result> evolution = new List<Result>(); // Création liste vide de portefeuilles

            string[] shareID = {opt.UnderlyingShare.Id};
            List<DataFeed> data = dataFeedProvider.GetDataFeed(shareID, testStart, opt.Maturity); // Données totales

            /* Calcul de la composition du portefeuille au début */
            List<DataFeed> estimationData = data.GetRange(0, estimationPeriod); 
            var (sigma, spot) = getVolatilityAndSpot(estimationData, estimationPeriod);
            DateTime pricingDate = estimationData.Last().Date;
            PricingResults pricingResult = pricer.Price(opt, pricingDate, 365, spot, sigma);
            double delta = pricingResult.Deltas[0];
            double optPrice = pricingResult.Price;
            HedgingPortfolio firstPortfolio = new HedgingPortfolio(delta * spot, optPrice - delta * spot);

            /* Ajout du resultat à la liste des évolutions */
            evolution.Add(new Result(firstPortfolio,
                                     firstPortfolio.RiskyAsset + firstPortfolio.RiskyAsset,
                                     optPrice - opt.Strike,
                                     pricingDate));

            /* Calcul des portefeuilles sur toute la période */
            int i = 0;
            DateTime pricingPeriodStart = data[estimationPeriod + i * rebalancingPeriod].Date;
            pricingDate = data[estimationPeriod + (i + 1) * rebalancingPeriod].Date;

            while (DateTime.Compare(pricingDate, testEnd) < 0)
            {
                List<DataFeed> pricingData = data.GetRange(estimationPeriod + i * rebalancingPeriod,
                    rebalancingPeriod);
                (sigma, spot) = getVolatilityAndSpot(pricingData, rebalancingPeriod);


                double portfolioValue = delta * spot + evolution.Last().Portfolio.NonRiskyAsset * Math.Exp(
                    r * DayCount.CountBusinessDays(pricingPeriodStart, pricingDate)/ 365);

                pricingResult = pricer.Price(opt, pricingDate, 360, spot, sigma);
                delta = pricingResult.Deltas[0];
                optPrice = pricingResult.Price;

                /* Ajout des resultats à la liste des évolutions */
                HedgingPortfolio newPortfolio = new HedgingPortfolio(delta * spot, portfolioValue - delta * spot);

                evolution.Add(new Result(newPortfolio,
                              portfolioValue,
                              optPrice - opt.Strike,
                              pricingDate));

                i += 1;
                pricingPeriodStart = pricingDate;
                pricingDate = data[estimationPeriod + (i + 1) * rebalancingPeriod].Date;
            }


            return evolution;
        }


        protected (double, double) getVolatilityAndSpot(List<DataFeed> data, int size)
        {
            double vol, spot;
            Decimal[] values = new Decimal[size];
            int i = 0;
            foreach (DataFeed d in data)
            {
                values[i] = d.PriceList.Values.First();
                i++;
            }
            spot = Decimal.ToDouble(values[size - 1]);
            vol = ComputeSd(values, size);
            return (vol, spot);
        }

        protected double ComputeSd(Decimal[] values, int size)
        {
            int i;
            decimal moyenne = 0;
            double moy, var = 0;
            for (i = 0; i < size; i++)
            {
                moyenne += values[i];
            }
            moy = Decimal.ToDouble(moyenne / size);
            for (i = 0; i < size; i++)
            {
                var += Math.Pow(moy - Decimal.ToDouble(values[i]), 2);
            }
            var = Math.Sqrt(var);
            return var;
        }
    }
}
