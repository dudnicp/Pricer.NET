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
using System.Windows;
using PricingApp.viewModel;

namespace PricingApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private OptionListViewModel optionListViewModel;
        public OptionListViewModel OptionListViewModel
        {
            get
            {
                return optionListViewModel;
            }
            set
            {
                if(value != optionListViewModel)
                {
                    optionListViewModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OptionListViewModel"));
                }
            }
        }

        public MainWindowViewModel()
        {
            optionListViewModel = new OptionListViewModel();
        }
    }
}
