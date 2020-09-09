using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class TestData : INotifyPropertyChanged
    {
        private OptionData testedOptionData;
        private DateTime testStart;
        private DateTime testEnd;
        private IDataFeedProvider selectedDataFeedProvider;
        private ObservableCollection<IDataFeedProvider> aviableDataFeedProviders;
        private int rebalancingPeriod;
        private int estimationPeriod;

        public event PropertyChangedEventHandler PropertyChanged;

        public OptionData TestedOptionData
        {
            get
            {
                return testedOptionData;
            }
            set
            {
                if(value != testedOptionData)
                {
                    testedOptionData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestedOptionData"));
                }
            }
        }

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

        public IDataFeedProvider SelectedDataFeedProvider
        {
            get
            {
                return selectedDataFeedProvider;
            }
            set
            {
                if (value != selectedDataFeedProvider)
                {
                    selectedDataFeedProvider = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDataFeedProvider"));
                }
            }
        }

        public ObservableCollection<IDataFeedProvider> AviableDataFeedProviders
        {
            get
            {
                return aviableDataFeedProviders;
            }
            set
            {
                if(value != aviableDataFeedProviders)
                {
                    aviableDataFeedProviders = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AviableDataFeedProviders"));
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

        public TestData(OptionData optData)
        {
            TestedOptionData = optData;
        }

        public void InitDefault()
        {
            TestStart = new DateTime(2020, 1, 1);
            TestEnd = new DateTime(2020, 12, 31);
            AviableDataFeedProviders = new ObservableCollection<IDataFeedProvider>() { new SimulatedDataFeedProvider()};
            SelectedDataFeedProvider = AviableDataFeedProviders.First();
            RebalancingPeriod = 60;
            EstimationPeriod = 30;
        }
    }
}
