using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class Result
    {
        private HedgingPortfolio portfolio;
        private double portfolioValue;
        private double optionPayOff;
        private DateTime date;

        public HedgingPortfolio Portfolio { get { return portfolio; } }
        public double PortfolioValue { get { return portfolioValue; } }
        public double OptionPayOff { get { return optionPayOff; } }
        public DateTime Date { get { return date; } }


        public Result(HedgingPortfolio portfolio, double portfolioValue, double optionPayOff, DateTime date)
        {
            this.portfolio = portfolio;
            this.portfolioValue = portfolioValue;
            this.optionPayOff = optionPayOff;
            this.date = date;
        }
    }
}
