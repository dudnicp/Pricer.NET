using PricingApp.model;
using PricingLibrary.FinancialProducts;
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
                        try
                        {
                            if(OptionData == null)
                            {
                                throw new Exception("Aucune Option n'a été selectionnée pour être modifiée");
                            }
                            if(OptionData.Strike < 0)
                            {
                                throw new Exception("Strike invalide");
                            }
                            if(OptionData.UnderlyingShares.Count == 0)
                            {
                                throw new Exception("L'option n'a aucun sous-jacent associé");
                            }
                            double sum = 0;
                            foreach(ShareAndWeight shw in OptionData.UnderlyingShares)
                            {
                                sum += shw.Weight;
                            }
                            if(sum != 1)
                            {
                                throw new Exception("La somme des poids des options n'est pas 1");
                            }
                            OptionData.copy(TempOptionData);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                }
                return saveChanges;
            }
        }
    }
}
