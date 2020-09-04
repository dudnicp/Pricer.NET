using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetPricing.services
{
    class DataTypeServices
    {
        public static List<string> getAviableDataTypes()
        {
            return new List<string>() {"Simulated", "Historical", "Semi-Historical"};
        }
    }
}
