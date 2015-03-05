using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleNew
{
    public class Normaliz
    {

        const double H0 = 70.0;
       
        double c_H0 = (3*Math.Pow(10,5)) / H0;

        public List<double> normilizeList()
        {

            List<double> normolizeNumbers = new List<double>();

            double mscrGuess = (5 * Math.Log10(c_H0)) + 25;
                   mscrGuess= Math.Round(mscrGuess,4);
            double mscrLo = mscrGuess - 0.2;
            double mscrHi = mscrGuess + 0.2;
            const double mscr_step = 0.005;

            for (double i = mscrLo; i < mscrHi; i += mscr_step)
            {
                normolizeNumbers.Add(i);
            }
            return normolizeNumbers;
        }


    }
}
