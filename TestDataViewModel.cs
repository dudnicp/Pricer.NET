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
        private SeriesCollection seriesCollection2;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string str="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(str));
        }

        public SeriesCollection SeriesCollection { get { return seriesCollection; } }
        public SeriesCollection SeriesCollection2 { get { return seriesCollection2; } }



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

            Share share1 = new Share("ACCOR SA", "AC FP");
            Share share2 = new Share("ALSTOM", "ALO FP");
            Share[] underlyingAsset = new Share[]{share1, share2 };
            VanillaCall opt0 = new VanillaCall("option", share1, new DateTime(2180, 2, 2), 5);
            BasketOption opt = new BasketOption("option", underlyingAsset, new double []{ 0.7, 0.3 }, new DateTime(2030, 12, 31), 10);

            //BasketOptionPricer pricer = new BasketOptionPricer(opt);
            var pricer = new VanillaCallPricer(opt0);

            PortfolioManager manager = new PortfolioManager(pricer, new SimulatedDataFeedProvider());

            // portfolio value 
            ChartValues <DateTimePoint> chartValues = new ChartValues<DateTimePoint>();
            List<TrackedResults> trackedList = manager.computePortfolioEvolution(new DateTime(2020, 1, 1), new DateTime(2025, 12, 1), 100, 20);
            // option value
            ChartValues<DateTimePoint> chartValues2 = new ChartValues<DateTimePoint>();
            // tracking error
            ChartValues<DateTimePoint> chartValues3 = new ChartValues<DateTimePoint>();
            ChartValues<DateTimePoint> chartValues4 = new ChartValues<DateTimePoint>();


            foreach(TrackedResults res in trackedList)
            {
                chartValues.Add(new DateTimePoint(res.Date, res.PortfolioValue));
                chartValues2.Add(new DateTimePoint(res.Date, res.Payoff));
                chartValues3.Add(new DateTimePoint(res.Date, res.TrackingError));
                chartValues4.Add(new DateTimePoint(res.Date, (res.PortfolioValue - res.Payoff) / res.TrackingError));
            }

            
            seriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries // portfolio value
                {
                    Title = "Portfolio",
                    Values = chartValues,
                    PointGeometrySize = 5
                },
                new LineSeries // payoff value
                {
                    Title = "Payoff",
                    Values = chartValues2,
                    PointGeometrySize = 5
                },
                new LineSeries // optionPrice
                {
                    Title = "Option",
                    Values = chartValues4,
                    PointGeometrySize = 5
                },

            };

            seriesCollection2 = new SeriesCollection(dayConfig)
            {

                new LineSeries // tracking error value
                {
                    Title = "Tracking Error",
                    Values = chartValues3,
                    PointGeometrySize = 5
                },
            };

            Formatter = value => new DateTime((long)value).ToString("d");
        }
        public Func<double, string> Formatter { get; set; }

    }
}
