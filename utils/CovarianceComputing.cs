/*
 *  Copyright (c) 2014 Raise Partner
 *  26, rue Gustave Eifel, 38000 Grenoble, France
 *  All rights reserved.
 *
 *  This software is the confidential and proprietary information
 *  of Raise Partner. You shall not disclose such Confidential
 *  Information and shall use it only in accordance with the terms
 *  of the licence agreement you entered into with Raise Partner.
 *  
 * */

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PricingApp.utils
{
    class CovarianceComputing
    {
        // import WRE dlls
        [DllImport("wre-modeling-c-4.1.dll", EntryPoint = "WREmodelingCov", CallingConvention = CallingConvention.Cdecl)]

        // declaration
        public static extern int WREmodelingCov(
            ref int returnsSize,
            ref int nbSec,
            double[,] secReturns,
            double[,] covMatrix,
            ref int info
        );

        public static double[,] computeCovarianceMatrix(double[,] returns)
        {
            int dataSize = returns.GetLength(0);
            int nbAssets = returns.GetLength(1);
            double[,] covMatrix = new double[nbAssets, nbAssets];
            int info = 0;
            int res;
            res = WREmodelingCov(ref dataSize, ref nbAssets, returns, covMatrix, ref info);
            if (res != 0)
            {
                if (res < 0)
                    throw new Exception("ERROR: WREmodelingCov encountred a problem. See info parameter for more details");
                else
                    throw new Exception("WARNING: WREmodelingCov encountred a problem. See info parameter for more details");
            }
            return covMatrix;
        }        
    }
}

