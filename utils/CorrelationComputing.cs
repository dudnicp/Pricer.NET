using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PricingApp.utils
{
    class CorrelationComputing
    {
        // import WRE dlls
        [DllImport("wre-ensimag-c-4.1.dll", EntryPoint = "WREmodelingCorr", CallingConvention = CallingConvention.Cdecl)]

        // declaration
        public static extern int WREmodelingCorr(
            ref int returnsSize, //nbValues
            ref int nbSec, //nbAssets
            double[,] secReturns, //assetsReturns
            double[,] corrMatrix, //cov
            ref int info
        );

        public static double[,] computeCorrelationMatrix(double[,] returns)
        {
            int dataSize = returns.GetLength(0); //nbValues, number of lines
            int nbAssets = returns.GetLength(1); //nbAssets
            double[,] corrMatrix = new double[nbAssets, nbAssets];
            int info = 0;
            int res;
            res = WREmodelingCorr(ref dataSize, ref nbAssets, returns, corrMatrix, ref info);
            if (res != 0)
            {
                if (res < 0)
                    throw new Exception("ERROR: WREmodelingCorr encountred a problem. See info parameter for more details");
                else
                    throw new Exception("WARNING: WREmodelingCorr encountred a problem. See info parameter for more details");
            }
            return corrMatrix;
        }
    }
}
