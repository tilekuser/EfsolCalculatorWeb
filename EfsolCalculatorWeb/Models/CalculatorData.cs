using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfsolCalculatorWeb.Models
{
    public class CalculatorData
    {
        public double FirstValue { get; set; }
        public double SecondValue { get; set; }
        public char Action { get; set; }
        public double Result { get; set; }
        public List<double> valueForSolutions { get; set; }
    }
}
