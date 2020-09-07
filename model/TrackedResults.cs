using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class TrackedResults : INotifyPropertyChanged
    {
        private HedgingPortfolio portfolio = new HedgingPortfolio();
        private double portfolioValue = 0;
        private double payoff = 0;
        private DateTime date = new DateTime();
        private double trackingError = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public HedgingPortfolio Portfolio 
        { 
            get 
            { 
                return portfolio; 
            } 
            
            set
            {
                if(portfolio != value)
                {
                    portfolio = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double PortfolioValue
        {
            get
            {
                return portfolioValue;
            }

            set
            {
                if (portfolioValue != value)
                {
                    portfolioValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double Payoff
        {
            get
            {
                return payoff;
            }

            set
            {
                if (payoff != value)
                {
                    payoff = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                if (date != value)
                {
                    date = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public double TrackingError
        {
            get
            {
                return trackingError;
            }

            set
            {
                if (trackingError != value)
                {
                    trackingError = value;
                    NotifyPropertyChanged();
                }
            }
        }

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
