using PricingApp.model;
using PricingApp.services;
using PricingApp.services.optionPricers;
using PricingApp.view;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static PricingApp.model.OptionData;

namespace PricingApp.viewModel
{
    public class TestDataViewModel : INotifyPropertyChanged
    {
        private TestData testData;

        public event PropertyChangedEventHandler PropertyChanged;

        public TestData TestData
        {
            get
            {
                return testData;
            }
            set
            {
                if(value != testData)
                {
                    testData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestData"));
                }
            }
        }

        private ICommand launchTest;
        public ICommand LaunchTest
        {
            get
            {
                if(launchTest == null)
                {
                    launchTest = new RelayCommand<IDataFeedProvider>(dataFeedProvider => 
                    {
                        GraphWindow graph = new GraphWindow();

                        List<TrackedResults> results = new List<TrackedResults>();

                        if(testData.TestedOptionData.UnderlyingShares.Count == 1)
                        {
                            Share share = TestData.TestedOptionData.UnderlyingShares.First().Share;
                            VanillaCall option = new VanillaCall(TestData.TestedOptionData.Name, 
                                share, 
                                TestData.TestedOptionData.Maturity,
                                TestData.TestedOptionData.Strike) ;
                            VanillaCallPricer pricer = new VanillaCallPricer(option);
                            PortfolioManager manager = new PortfolioManager(pricer, dataFeedProvider);
                            results = manager.computePortfolioEvolution(TestData.TestStart,
                            TestData.TestEnd,
                            TestData.RebalancingPeriod,
                            TestData.EstimationPeriod);
                        }
                        else if(testData.TestedOptionData.UnderlyingShares.Count > 1)
                        {
                            Share[] shares = new Share[TestData.TestedOptionData.UnderlyingShares.Count];
                            double[] weight = new double[TestData.TestedOptionData.UnderlyingShares.Count];
                            int i = 0;
                            foreach(ShareAndWeight share in TestData.TestedOptionData.UnderlyingShares)
                            {
                                shares[i] = share.Share;
                                weight[i] = share.Weight;
                                i++;
                            }

                            BasketOption option = new BasketOption(TestData.TestedOptionData.Name,
                                shares,
                                weight,
                                TestData.TestedOptionData.Maturity,
                                TestData.TestedOptionData.Strike);
                            BasketOptionPricer pricer = new BasketOptionPricer(option);
                            PortfolioManager manager = new PortfolioManager(pricer, dataFeedProvider);
                            results = manager.computePortfolioEvolution(TestData.TestStart,
                            TestData.TestEnd,
                            TestData.RebalancingPeriod,
                            TestData.EstimationPeriod);
                        }

                        graph.DataContext = new GraphViewModel(results);
                        graph.Show();

                    });
                }
                return launchTest;
            }
        }
    }
}
