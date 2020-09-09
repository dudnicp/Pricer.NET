using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.viewModel
{
    class DataChartViewModel
    {
        //public System.DateTime DateTime { get; set; }
        public DateTime Date { get; set; }
        public double Value { set; get; }

        public DataChartViewModel(DateTime date, double val)
        {
            this.Date = date;
            this.Value = val;
        }
    }
}
