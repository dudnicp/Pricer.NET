using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using PricingApp.model;
using PricingApp.services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PricingApp
{
    public class TestDataViewModel : INotifyPropertyChanged
    {
        private string optionName;
        private double optionStrike;
        private DateTime optionMaturity;
        private DateTime testStart;
        private DateTime testEnd;
        private IDataFeedProvider dataProvider;
        private int rebalancingPeriod;
        private int estimationPeriod;
        private ObservableCollection<Share> optionUnderlyingShares;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public string OptionName
        {
            get
            {
                return optionName;
            }
            set
            {
                if(value != optionName)
                {
                    optionName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double OptionStrike
        {
            get
            {
                return optionStrike;
            }
            set
            {
                if (value != optionStrike)
                {
                    optionStrike = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime OptionMaturity
        {
            get
            {
                return optionMaturity;
            }
            set
            {
                if (value != optionMaturity)
                {
                    optionMaturity = value;
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
                }
            }
        }

        public IDataFeedProvider DataProvider
        {
            get
            {
                return dataProvider;
            }
            set
            {
                if (value != dataProvider)
                {
                    dataProvider = value;
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
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
                    NotifyPropertyChanged();
                }
            }
        }

        public ObservableCollection<Share> OptionUnderlyingShares
        {
            get
            {
                return optionUnderlyingShares;
            }
            set
            {
                if(value != optionUnderlyingShares)
                {
                    optionUnderlyingShares = value;
                    NotifyPropertyChanged();
                }
            }
        } 

        private ICommand addUnderlyingShare;
        public ICommand AddUnderlyingShare
        {
            get
            {
                if (addUnderlyingShare == null)
                {
                    addUnderlyingShare = new RelayCommand<object>((obj) => OptionUnderlyingShares.Add(new Share("name", "id")));
                }
                return addUnderlyingShare;
            }
        }

        public TestDataViewModel()
        {
            optionUnderlyingShares = new ObservableCollection<Share>();
        }
    }
}
