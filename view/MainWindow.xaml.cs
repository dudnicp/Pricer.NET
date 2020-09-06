using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PricingApp.model;
using PricingApp.view;
using PricingAPp.viewModel;

namespace PricingApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, View
    {
        public MainWindow()
        {
            InitializeComponent();
            PortfolioViewModel vm = new PortfolioViewModel(this);
            vm.runApp();
        }

        public void update(Result res)
        {
            resultBox.Text += "--------------\n";
            resultBox.Text += "Risky Asset : " + res.Portfolio.RiskyAsset + "\n";
            resultBox.Text += "Non Risky Asset : " + res.Portfolio.NonRiskyAsset + "\n";
        }
    }
}
