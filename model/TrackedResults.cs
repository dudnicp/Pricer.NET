using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class TrackedResults
    {
        private HedgingPortfolio portfolio = new HedgingPortfolio();
        private double portfolioValue = 0;
        private double payoff = 0;
        private DateTime date = new DateTime();
        private double trackingError = 0;

        public HedgingPortfolio Portfolio { get { return portfolio; } }
        public double PortfolioValue { get { return portfolioValue; } set { portfolioValue = value; } }
        public double Payoff { get { return payoff; } set { payoff = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public double TrackingError {  get { return trackingError; } set { trackingError = value; } }


        public TrackedResults(HedgingPortfolio portfolio, double portfolioValue, double optionPayOff, DateTime date, double trackingError)
        {
            this.portfolio = portfolio;
            this.portfolioValue = portfolioValue;
            this.payoff = optionPayOff;
            this.date = date;
            this.trackingError = trackingError;
        }

        public TrackedResults()
        {
        }
    }
}
