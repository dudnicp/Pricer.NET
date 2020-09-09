using PricingApp.model;
using PricingLibrary.FinancialProducts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PricingApp.viewModel
{
    public class OptionDataViewModel : INotifyPropertyChanged
    {
        private OptionData optionData;
        private OptionData tempOptionData;

        public event PropertyChangedEventHandler PropertyChanged;

        public OptionData OptionData
        {
            get
            {
                return optionData;
            }
            set
            {
                if(value != optionData)
                {
                    optionData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OptionData"));
                }
            }
        }

        public OptionData TempOptionData
        {
            get
            {
                return tempOptionData;
            }
            set
            {
                if (value != tempOptionData)
                {
                    tempOptionData = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TempOptionData"));
                }
            }
        }

        private ICommand addUnderlyingShare;
        public ICommand AddUnderlyingShare
        {
            get
            {
                if(addUnderlyingShare == null)
                {
                    addUnderlyingShare = new RelayCommand<Share>(share => 
                    {
                        TempOptionData.addUnderlyingShare(share);
                    });
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
                    removeUnderlyingShare = new RelayCommand<OptionData.ShareAndWeight>(share =>
                    {
                        TempOptionData.removeUnderlyingShare(share.Share);
                    });
                }
                return removeUnderlyingShare;
            }
        }

        private ICommand saveChanges;
        public ICommand SaveChanges
        {
            get
            {
                if(saveChanges == null)
                {
                    saveChanges = new RelayCommand<Object>(obj => 
                    {
                        OptionData.copy(TempOptionData);
                    });
                }
                return saveChanges;
            }
        }
    }
}
