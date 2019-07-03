﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace ODE.Astronomy
{
    public class VsopParser
    {
        // Structure used internally to store each term.
        private struct Term
        {
            public double A, B, C;

            public double Calculate(double tau)
            {
                return (A * Math.Cos(B + C * tau));
            }
        }

        // termsArray basically stores all VSOP data.
        private Term[,][] termsArray;

        public VsopParser(string strPlanetAbbreviation)
        {
            Uri uri = new Uri("/ODE.Astronomy;component/Data/VSOP87D." + strPlanetAbbreviation, UriKind.Relative);
            StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);

            using (StreamReader reader = new StreamReader(resourceInfo.Stream))
            {
                string strLine;

                strLine = reader.ReadLine();
                while (strLine != null)
                {
                    // variable is 1, 2, or 3 corresponding to 
                    // L (ecliptical longitude), B (ecliptical latitude), or R (radius).
                    int variable = Int32.Parse(strLine.Substring(41, 1));

                    // terms is the number of lines that follow.
                    int terms = Int32.Parse(strLine.Substring(60, 7));

                    // power is the exponent of time.
                    int power = Int32.Parse(strLine.Substring(59, 1));

                    // Allocate a new array for all the terms that follow.
                    Term[] termArray = new Term[terms - 1 + 1];

                    // Fill up the termArray.
                    for (var i = 0; i <= terms - 1; i++)
                    {
                        strLine = reader.ReadLine();
                        termArray[i].A = double.Parse(strLine.Substring(79, 18));
                        termArray[i].B = double.Parse(strLine.Substring(97, 14));
                        termArray[i].C = double.Parse(strLine.Substring(111, 20));
                    }

                    // Save it as part of the big array.
                    termsArray[variable - 1, power] = termArray;
                    strLine = reader.ReadLine();
                }
            }
        }

        public double GetLongitude(double tau)
        {
            return Calculate(0, tau);
        }

        public double GetLatitude(double tau)
        {
            return Calculate(1, tau);
        }

        public double GetRadius(double tau)
        {
            return Calculate(2, tau);
        }

        private double Calculate(int variable, double tau)
        {
            double final = 0;
            double tauPowered = 1;

            for (var power = 0; power <= 5; power++)
            {
                Term[] termArray = termsArray[variable, power];

                if (termArray != null)
                {
                    double accum = 0;

                    for (var term = 0; term <= termArray.Length - 1; term++)
                        accum += termArray[term].Calculate(tau);

                    final += accum * tauPowered;
                }
                tauPowered *= tau;
            }
            return final;
        }
    }
}
