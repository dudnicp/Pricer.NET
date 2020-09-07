using PricingLibrary.Computations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PricingApp.model
{
    public class CompletePricingResults
    {
        public double[] Deltas { get; }

        public double Price { get; }
        public double[] Spots { get; }

        public CompletePricingResults(PricingResults res, double[] spots)
        {
            Deltas = res.Deltas;
            Price = res.Price;
            Spots = spots;
        }
    }
}
