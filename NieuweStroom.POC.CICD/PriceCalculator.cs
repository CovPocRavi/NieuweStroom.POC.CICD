using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NieuweStroom.POC.CICD
{
    public class PriceCalculator
    {
        public PriceCalculator()
        {

        }
        
        public int CalculatePrice(int i, int j)
        {
            int k = i * j;
            return (k);
        }

    }
}
