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
        private SeriesCollection seriesCollection;
        private SeriesCollection seriesCollection2;

        public event PropertyChangedEventHandler PropertyChanged;

        public SeriesCollection SeriesCollection { get { return seriesCollection; } }
        public SeriesCollection SeriesCollection2 { get { return seriesCollection2; } }
        public TestDataViewModel()
        {
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

            List<TrackedResults> trackedList = manager.computePortfolioEvolution(new DateTime(2020, 1, 1), new DateTime(2025, 12, 1), 100, 20);





            ChartValues <DateTimePoint> portfolioValues = new ChartValues<DateTimePoint>();
            ChartValues<DateTimePoint> payoffValues = new ChartValues<DateTimePoint>();
            ChartValues<DateTimePoint> trackingErrorValues = new ChartValues<DateTimePoint>();
            ChartValues<DateTimePoint> optionPriceValues = new ChartValues<DateTimePoint>();

            foreach(TrackedResults res in trackedList)
            {
                portfolioValues.Add(new DateTimePoint(res.Date, res.PortfolioValue));
                payoffValues.Add(new DateTimePoint(res.Date, res.Payoff));
                trackingErrorValues.Add(new DateTimePoint(res.Date, res.TrackingError));
                optionPriceValues.Add(new DateTimePoint(res.Date, (res.PortfolioValue - res.Payoff) / res.TrackingError));
            }

            seriesCollection = new SeriesCollection(dayConfig)
            {
                new LineSeries
                {
                    Title = "Portfolio",
                    Values = portfolioValues,
                    PointGeometrySize = 5
                },
                new LineSeries
                {
                    Title = "Payoff",
                    Values = payoffValues,
                    PointGeometrySize = 5
                },
                new LineSeries
                {
                    Title = "Option",
                    Values = optionPriceValues,
                    PointGeometrySize = 5
                },

            };

            seriesCollection2 = new SeriesCollection(dayConfig)
            {

                new LineSeries
                {
                    Title = "Tracking Error",
                    Values = trackingErrorValues,
                    PointGeometrySize = 5
                },
            };

            Formatter = value => new DateTime((long)value).ToString("d");
        }
        public Func<double, string> Formatter { get; set; }

    }
}
