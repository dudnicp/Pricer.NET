using PricingLibrary.FinancialProducts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class OptionData : INotifyPropertyChanged
    {
        public class ShareAndWeight
        {
            public Share Share { get; set; }
            public double Weight { get; set; }
        }

        private string name;
        private double strike;
        private DateTime maturity;
        private ObservableCollection<ShareAndWeight> underlyingShares;
        private ObservableCollection<Share> aviableShares; 

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public double Strike
        {
            get
            {
                return strike;
            }
            set
            {
                if (value != strike)
                {
                    strike = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Strike"));
                }
            }
        }

        public DateTime Maturity
        {
            get
            {
                return maturity;
            }
            set
            {
                if (value != maturity)
                {
                    maturity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Maturity"));
                }
            }
        }

        public ObservableCollection<ShareAndWeight> UnderlyingShares
        {
            get
            {
                return underlyingShares;
            }
            set
            {
                if (value != underlyingShares)
                {
                    underlyingShares = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UnderlyingShares"));
                }
            }
        }

        public ObservableCollection<Share> AviableShares
        {
            get
            {
                return aviableShares;
            }
            set
            {
                if (value != aviableShares)
                {
                    aviableShares = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AviableShares"));
                }
            }
        }
    }
}
