using PricingApp.model;
using PricingApp.services;
using PricingApp.services.optionPricers;
using PricingApp.view;
using PricingLibrary.Computations;
using PricingLibrary.FinancialProducts;
using PricingLibrary.Utilities;
using PricingLibrary.Utilities.MarketDataFeed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                        try
                        {
                            if(TestData == null)
                            {
                                throw new Exception("Aucune option n'a été séléctionnée pour être testée");
                            }
                            if(DateTime.Compare(TestData.TestEnd, TestData.TestStart) <= 0)
                            {
                                throw new Exception("Date de fin de test avant le début du test");
                            }
                            if (DateTime.Compare(TestData.TestedOptionData.Maturity, TestData.TestEnd) < 0)
                            {
                                throw new Exception("Date de maturité de l'option avant la fin du test");
                            }
                            if(TestData.RebalancingPeriod <= 0)
                            {
                                throw new Exception("Période de rebalancement invalide");
                            }
                            if (TestData.EstimationPeriod <= 0)
                            {
                                throw new Exception("Période d'estimation invalide");
                            }

                            if (DayCount.CountBusinessDays(TestData.TestStart, TestData.TestEnd) <= TestData.RebalancingPeriod)
                            {
                                throw new Exception("La période de rebalancement dépasse le nombre de jours ouvrés de la période de test");
                            }
                            if(TestData.RebalancingPeriod < TestData.EstimationPeriod)
                            {
                                throw new Exception("La période d'estimation est plus grande que la période de rebalancement");
                            }


                            GraphWindow graph = new GraphWindow();

                            List<TrackedResults> results = new List<TrackedResults>();
                            if (testData.TestedOptionData.UnderlyingShares.Count == 1)
                            {
                                Share share = TestData.TestedOptionData.UnderlyingShares.First().Share;
                                VanillaCall option = new VanillaCall(TestData.TestedOptionData.Name,
                                    share,
                                    TestData.TestedOptionData.Maturity,
                                    TestData.TestedOptionData.Strike);
                                VanillaCallPricer pricer = new VanillaCallPricer(option);
                                PortfolioManager manager = new PortfolioManager(pricer, dataFeedProvider);
                                results = manager.computePortfolioEvolution(TestData.TestStart,
                                TestData.TestEnd,
                                TestData.RebalancingPeriod,
                                TestData.EstimationPeriod);
                            }
                            else if (testData.TestedOptionData.UnderlyingShares.Count > 1)
                            {
                                Share[] shares = new Share[TestData.TestedOptionData.UnderlyingShares.Count];
                                double[] weight = new double[TestData.TestedOptionData.UnderlyingShares.Count];
                                int i = 0;
                                foreach (ShareAndWeight share in TestData.TestedOptionData.UnderlyingShares)
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
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                }
                return launchTest;
            }
        }
    }
}
