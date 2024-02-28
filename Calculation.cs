using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211Lab4
{
    internal class Calculation
    {
        //All three properties can be null.
        //Whether properties are null is used to track which input the calculator expects.
        public double? Number1 { get; set; }
        public double? Number2 { get; set; }
        public string? Operation {  get; set; }

        public Calculation() 
        {
            this.Number1 = null;
            this.Number2 = null;
            this.Operation = null;
        }

        public Calculation(double? number1, double? number2, string? operation)
        {
            Number1 = number1;
            Number2 = number2;
            Operation = operation;
        }

        public string GetResult()
        {
            if (Number1 == null || Number2 == null || Operation == null)
            {
                return "Invalid Operaiton.";
            }
            if (Number2 == 0 && Operation == "÷")
            {
                return "Cannot divide by zero.";
            }
            switch (Operation)
            {
                //Format numbers to 10 digits: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings
                case "+":
                    return $"{Number1 + Number2:G10}";
                case "-":
                    return $"{Number1 - Number2:G10}";
                case "×":
                    return $"{Number1 * Number2:G10}";
                case "÷":
                    return $"{Number1 / Number2:G10}";
                default:
                    return "Invalid Operaiton.";
            }
        }
    }
}
