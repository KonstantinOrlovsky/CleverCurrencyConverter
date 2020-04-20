using System;
using System.Collections.Generic;

namespace CurrencyConvert
{
    internal sealed class PartsFinder
    {
        public List<string> CurrenciesWithValues { get; set; } = new List<string>();
        public List<string> OperatorsList { get; set; } = new List<string>();
        public string LeftPartBeforeConversion { get; set; } = "";
        public string RightPartWithConversionInfo { get; set; } = "";
        public PartsFinder(string text)
        {
            Split(text);
        }

        public void Split(string text)
        {
            var parts = text.Split(',');
            LeftPartBeforeConversion = parts[0];
            RightPartWithConversionInfo = parts[1];
            SplitCurrenciesWithValues(LeftPartBeforeConversion);
        }

        public void SplitCurrenciesWithValues(string leftPart)
        {
            int z = 0;
            var elements = leftPart.Split(' ');
            foreach (var x in elements)
            {
                z++;
                if (z % 2 != 0)
                {
                    CurrenciesWithValues.Add(x);
                }
                else OperatorsList.Add(x);
            }
            if (z % 2 == 0)
            {
                throw new ArgumentException($"{Constants.WrongInputMessage}:Некорректно введен оператор");
            }
        }
    }
}
