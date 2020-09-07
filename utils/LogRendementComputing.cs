using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;

namespace PricingApp.utils
{
    class LogRendementComputing
    {
        // import WRE dlls
        [DllImport("wre-ensimag-c-4.1.dll", EntryPoint = "WREmodelingLogReturns", CallingConvention = CallingConvention.Cdecl)]

        // declaration
        public static extern int WREmodelingLogReturns(
            ref int returnsSize,
            ref int nbSec,
            double[,] secReturns,
            double[,] assetsValues,
            int horizon,
            ref int info
        );

        public static double[,] computeCovarianceMatrix(double[,] returns)
        {
            int dataSize = returns.GetLength(0);
            int nbAssets = returns.GetLength(1);
            int horizon = dataSize - 1;
            double[,] assetsValues = new double[(dataSize - horizon), nbAssets];
            int info = 0;
            int res;
            res = WREmodelingLogReturns(ref dataSize, ref nbAssets, returns, assetsValues, horizon, ref info);
            if (res != 0)
            {
                if (res < 0)
                    throw new Exception("ERROR: WREmodelingLogReturns encountred a problem. See info parameter for more details");
                else
                    throw new Exception("WARNING: WREmodelingLogReturns encountred a problem. See info parameter for more details");
            }
            return assetsValues;
        }

        public static void dispMatrix(double[,] myassetsValues)
        {
            int n = myassetsValues.GetLength(0);

            Console.WriteLine("LogRendements");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(myassetsValues[i, j] + "\t");
                }
                Console.Write("\n");
            }
        }

        static void Main(string[] args)
        {
            // header
            Console.WriteLine("******************************");
            Console.WriteLine("*    WREmodelingLogReturns in C#   *");
            Console.WriteLine("******************************");

            // sample data
            double[,] returns = { {0.05, -0.1, 0.6}, {-0.001, -0.4, 0.56}, {0.7, 0.001, 0.12}, {-0.3, 0.2, -0.1},
                                {0.1, 0.2, 0.3}};

            // call WRE via computeCovarianceMatrix encapsulation
            double[,] myassetsValues = computeCovarianceMatrix(returns);

            // display result
            dispMatrix(myassetsValues);

            // ending the program            
            Console.WriteLine("\nType enter to exit");
            Console.ReadKey(true);
        }
    }
}
