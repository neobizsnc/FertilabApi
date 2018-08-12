using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FertilabApi.Models
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
