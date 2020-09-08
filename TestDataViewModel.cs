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
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using System.CodeDom;
using PricingApp.viewModel;
using PricingApp.services.optionPricers;

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
        private SeriesCollection seriesCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public SeriesCollection SeriesCollection { get { return seriesCollection; } }

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
                    NotifyPropertyChanged("OptionUnderlyingShares");
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

        private ICommand removeUnderlyingShare;
        public ICommand RemoveUnderlyingShare
        {
            get
            {
                if (removeUnderlyingShare == null)
                {
                    removeUnderlyingShare = new RelayCommand<Share>((share) => OptionUnderlyingShares.Remove(share));
                }
                return removeUnderlyingShare;
            }
        }



        public TestDataViewModel()
        {
            optionUnderlyingShares = new ObservableCollection<Share>();

            var dayConfig = Mappers.Xy<DataChartViewModel>()
                .X(dayModel => (double)dayModel.Date.Ticks / TimeSpan.FromDays(1).Ticks)
                .Y(dayModel => dayModel.Value);

            Share share = new Share("ACCOR SA", "AC FP");
            VanillaCall opt = new VanillaCall("option", share, new DateTime(2020, 12, 31), 10);
            VanillaCallPricer pricer = new VanillaCallPricer(opt);

            PortfolioManager manager = new PortfolioManager(pricer, new SimulatedDataFeedProvider());

            ChartValues <DataChartViewModel> chartValues = new ChartValues<DataChartViewModel>();

            foreach (TrackedResults res in
                    manager.computePortfolioEvolution
                    (new DateTime(2020, 1, 1), new DateTime(2020, 12, 1), 50, 30))
            {
                chartValues.Add(new DataChartViewModel(res.Date, res.PortfolioValue));
            }


            seriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries // payoff option
                {
                    Values = chartValues,
                    PointGeometrySize = 25
                },             
            };
            Formatter = value => new System.DateTime((long)(value * TimeSpan.FromDays(1).Ticks)).ToString("t");
        }
        public Func<double, string> Formatter { get; set; }

    }
}
