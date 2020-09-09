using PricingApp.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                    launchTest = new RelayCommand<object>(obj => 
                    { 
                        // TODO
                    });
                }
                return launchTest;
            }
        }
    }
}
