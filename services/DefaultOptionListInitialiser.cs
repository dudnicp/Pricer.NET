using PricingApp.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.services
{
    public class DefaultOptionListInitialiser
    {
        public static void InitOptionList(ICollection<OptionData> optionList)
        {
            OptionData option1 = new OptionData();
            option1.Name = "Call Vanille";
            option1.Strike = 10;
            option1.Maturity = new DateTime(2180, 2, 2);
            option1.addUnderlyingShare("EN FP");

            OptionData option2 = new OptionData();
            option2.Name = "Option Panier 1";
            option2.Strike = 9;
            option2.Maturity = new DateTime(2013, 6, 11);
            option2.addUnderlyingShare("AI FP", 0.3);
            option2.addUnderlyingShare("BN FP", 0.3);
            option2.addUnderlyingShare("CAP FP", 0.4);

            OptionData option3 = new OptionData();
            option3.Name = "Option Panier 2";
            option3.Strike = 41;
            option3.Maturity = new DateTime(2010, 4, 14);
            option3.addUnderlyingShare("AC FP", 0.25);
            option3.addUnderlyingShare("AI FP", 0.25);
            option3.addUnderlyingShare("CA FP", 0.25);
            option3.addUnderlyingShare("EN FP", 0.25);

            OptionData option4 = new OptionData();
            option4.Name = "Option Panier 3";
            option4.Strike = 9;
            option4.Maturity = new DateTime(2300, 5, 20);
            option4.addUnderlyingShare("AI FP", 0.3);
            option4.addUnderlyingShare("BN FP", 0.3);
            option4.addUnderlyingShare("CAP FP", 0.4);

            optionList.Add(option1);
            optionList.Add(option2);
            optionList.Add(option3);
            optionList.Add(option4);
        }
    }
}
