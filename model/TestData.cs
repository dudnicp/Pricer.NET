using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class TestData : INotifyPropertyChanged
    {
        private DateTime testStart;
        private DateTime testEnd;
        private IDataFeedProvider dataFeedProvider;
        private int rebalancingPeriod;
        private int estimationPeriod;

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime TestStart
        {
            get
            {
                return testStart;
            }
            set
            {
                if (value != testStart)
                {
                    testStart = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestStart"));
                }
            }
        }

        public DateTime TestEnd
        {
            get
            {
                return testEnd;
            }
            set
            {
                if (value != testEnd)
                {
                    testEnd = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestEnd"));
                }
            }
        }

        public IDataFeedProvider DataProvider
        {
            get
            {
                return dataFeedProvider;
            }
            set
            {
                if (value != dataFeedProvider)
                {
                    dataFeedProvider = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataFeedProvider"));
                }
            }
        }

        public int RebalancingPeriod
        {
            get
            {
                return rebalancingPeriod;
            }
            set
            {
                if (value != rebalancingPeriod)
                {
                    rebalancingPeriod = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RebalancingPeriod"));
                }
            }
        }

        public int EstimationPeriod
        {
            get
            {
                return estimationPeriod;
            }
            set
            {
                if (value != estimationPeriod)
                {
                    estimationPeriod = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EstimationPeriod"));
                }
            }
        }
    }
}
