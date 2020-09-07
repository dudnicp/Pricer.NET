using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace PricingApp.utils
{
    class VolatilityComputing
    {
        // import WRE dlls
        [DllImport("wre-ensimag-c-4.1.dll", EntryPoint = "WREanalysisExpostVolatility", CallingConvention = CallingConvention.Cdecl)]

        // declaration
        public static extern int WREanalysisExpostVolatility(
            ref int nbValues,
            double[,] portfolioReturns,
            double[,] expostVolatility,
            ref int info
        );

        public static double[,] computeExpostVolatility(double[,] returns)
        {
            int nbAssets = returns.GetLength(1);
            double[,] expostVolatility = new double[1, 1];
            int info = 0;
            int res;
            res = WREanalysisExpostVolatility(ref nbAssets, returns, expostVolatility, ref info);
            if (res != 0)
            {
                if (res < 0)
                    throw new Exception("ERROR: WREanalysisExpostVolatility encountred a problem. See info parameter for more details");
                else
                    throw new Exception("WARNING: WREanalysisExpostVolatility encountred a problem. See info parameter for more details");
            }
            return expostVolatility;
        }

        public static void dispMatrix(double[,] myExpostVolatility)
        {
            int n = myExpostVolatility.GetLength(0);

            Console.WriteLine("Volatility");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(myExpostVolatility[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }

    }
}
