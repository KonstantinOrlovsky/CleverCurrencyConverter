using System.Collections.Generic;

namespace CurrencyConvert
{
    public sealed class Calculator
    {
        private readonly Dictionary<string, decimal> _ratesRelations;

        public Calculator(Dictionary<string, decimal> ratesRelations)
        {
            _ratesRelations = ratesRelations;
        }
        public decimal GetResultFromExpression(string sourceExpressions)
        {
            PartsFinder partsFinder = new PartsFinder(sourceExpressions);
            CurrencyParser currencyParser = new CurrencyParser();
            var finalCurrency = currencyParser.ParseFinalCurrencyOperation(partsFinder.RightPartWithConversionInfo);
            decimal totalResult = GetConvertValueFromPart(partsFinder.CurrenciesWithValues[0], finalCurrency);
            for (int i = 0; i < partsFinder.OperatorsList.Count; i++)
            {
                switch (partsFinder.OperatorsList[i])
                {
                    case Constants.PlusMathOperation:
                        var convertedValueToPlus = GetConvertValueFromPart(partsFinder.CurrenciesWithValues[i + 1], finalCurrency);
                        totalResult += convertedValueToPlus;
                        break;
                    case Constants.MinusMathOperation:
                        var convertedValueToMinus = GetConvertValueFromPart(partsFinder.CurrenciesWithValues[i + 1], finalCurrency);
                        totalResult -= convertedValueToMinus;
                        break;
                }
            }
           
            return System.Math.Round(totalResult, Constants.NumberAfterPoint);
        }

        private decimal GetConvertValueFromPart(string part, string finalCurrency)
        {
            CurrencyParser currencyParser = new CurrencyParser();
            var currencyWithValue = part;
            var partsParser = new PartsParser(currencyWithValue);
            var currencyConverter = new CurrencyConverter(_ratesRelations);
            var convertedValue = currencyConverter.CalculateTotalValue(partsParser.GetValue(), partsParser.GetCurrency(),
                currencyParser.ParseCurrencyConversionOperation(currencyWithValue), finalCurrency);

            return convertedValue;
        }
    }
}