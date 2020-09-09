using PricingApp.model;
using PricingApp.services;
using PricingLibrary.FinancialProducts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace PricingApp.viewModel
{
    public class OptionListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<OptionData> optionList;
        private OptionDataViewModel selectedOptionViewModel;
        private TestDataViewModel testDataViewModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<OptionData> OptionList
        {
            get
            {
                return optionList;
            }
            set
            {
                if (value != optionList)
                {
                    optionList = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("OptionList"));
                }
            }
        }

        public OptionDataViewModel SelectedOptionViewModel
        {
            get
            {
                return selectedOptionViewModel;
            }
            set
            {
                if (value != selectedOptionViewModel)
                {
                    selectedOptionViewModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedOptionViewModel"));
                }
            }
        }

        public TestDataViewModel TestDataViewModel
        {
            get
            {
                return testDataViewModel;
            }
            set
            {
                if (value != testDataViewModel)
                {
                    testDataViewModel = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TestDataViewModel"));
                }
            }
        }

        public OptionListViewModel()
        {
            OptionList = new ObservableCollection<OptionData>();
            DefaultOptionListInitialiser.InitOptionList(OptionList);

            SelectedOptionViewModel = new OptionDataViewModel();
            TestDataViewModel = new TestDataViewModel();

        }

        private ICommand addOption; 
        public ICommand AddOption
        {
            get
            {
                if(addOption == null)
                {
                    addOption = new RelayCommand<Object>(obj =>
                    {
                        OptionData opt = new OptionData();
                        opt.InitDefault();
                        OptionList.Add(opt);
                    }); 
                }
                return addOption;
            }
        }

        private ICommand removeOption;
        public ICommand RemoveOption
        {
            get
            {
                if(removeOption == null)
                {
                    removeOption = new RelayCommand<OptionData>(opt =>
                    {
                        OptionList.Remove(opt);
                    });
                }
                return removeOption;
            }
        }

        private ICommand editOption;
        public ICommand EditOption
        {
            get
            {
                if (editOption == null)
                {
                    editOption = new RelayCommand<OptionData>(opt =>
                    {
                        SelectedOptionViewModel.OptionData = opt;
                        SelectedOptionViewModel.TempOptionData = new OptionData(opt);
                    });
                }
                return editOption;
            }
        }

        private ICommand testOption;
        public ICommand TestOption
        {
            get
            {
                if(testOption == null)
                {
                    testOption = new RelayCommand<OptionData>(optData =>
                    {
                        TestDataViewModel.TestData = new TestData(optData);
                        TestDataViewModel.TestData.InitDefault();
                    });
                }
                return testOption;
            }
        }
    }
}
