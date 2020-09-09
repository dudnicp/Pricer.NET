using PricingApp.services;
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

            public ShareAndWeight(Share share, double weight)
            {
                Share = share;
                Weight = weight;
            }
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

        public OptionData()
        {
            UnderlyingShares = new ObservableCollection<ShareAndWeight>();
            AviableShares = new ObservableCollection<Share>(DataBaseServices.getShares());
        }

        public OptionData(OptionData other)
        {
            copy(other);
        }

        public void InitDefault()
        {
            Name = "NouvelleOption";
            Strike = 10;
            Maturity = new DateTime(2030, 12, 31);
            addUnderlyingShare(AviableShares.First());
        }

        public void copy(OptionData other)
        {
            Name = other.Name;
            Strike = other.Strike;
            Maturity = new DateTime(other.Maturity.Ticks);
            AviableShares = new ObservableCollection<Share>(other.AviableShares);
            UnderlyingShares = new ObservableCollection<ShareAndWeight>(other.UnderlyingShares);
        }

        public void addUnderlyingShare(Share share, double weight = 1)
        {
            UnderlyingShares.Add(new ShareAndWeight(share, weight));
            AviableShares.Remove(share);
        }

        public void removeUnderlyingShare(Share share)
        {
            bool found = false;
            int i = 0;
            while (!found && i < UnderlyingShares.Count)
            {
                if(share == UnderlyingShares[i].Share)
                {
                    UnderlyingShares.Remove(UnderlyingShares[i]);
                    AviableShares.Add(share);
                    found = true;
                }
                i++;
            }
        }

        public void addUnderlyingShare(string Id, double weight = 1)
        {
            bool found = false;
            int i = 0;
            while (!found && i < AviableShares.Count)
            {
                if ((Id + "    ").Equals(AviableShares[i].Id))
                {
                    UnderlyingShares.Add(new ShareAndWeight(AviableShares[i], weight));
                    AviableShares.Remove(AviableShares[i]);
                    found = true;
                }
                i++;
            }
        }

    }
}
