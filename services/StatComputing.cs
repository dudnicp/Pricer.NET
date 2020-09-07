using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PricingLibrary.Utilities.MarketDataFeed;
using PricingApp.utils;


namespace PricingApp.services
{
    class StatComputing
    {
        // data is a list of DataFeed containing the underlying spots during the estimation window 
        // utiliser la fonction de conversion une fois dans le main et remplacer les entrées de fonctpn par un double[,]
       public static double[,] correlationMatrix(double[,] underlyingSpots)
        {
            // converting var to double
            // converting to log ??
            int dataSize = underlyingSpots.GetLength(0); 
            int nbAssets = underlyingSpots.GetLength(1); 
            int info = 0;
            //CorrelationComputing.WREmodelingCorr(ref dataSize, ref nbAssets, underlyingSpots, new double[nbAssets, nbAssets], ref info);
            double[,] correlationMatrix = CorrelationComputing.computeCorrelationMatrix(underlyingSpots);
            return correlationMatrix;
        }

        public static double[] volatilitiesComputing(double[,] underlyingSpots)
        {
            double[,] covarianceMatrix = CovarianceComputing.computeCovarianceMatrix(underlyingSpots);
            double[] volatilities = new double[covarianceMatrix.GetLength(0)];
            for(int i=0; i<volatilities.Count(); i++)
            {
                volatilities[i] = Math.Sqrt(covarianceMatrix[i, i]); 
            }            
            return volatilities;
        }
    }
}
