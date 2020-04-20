using System;
using System.Collections.Generic;

namespace CurrencyConvert
{
    internal sealed class CurrencyConverter
    {
        private readonly Dictionary<string, string> _currencyMathOperations = new Dictionary<string, string>
        {
            [$"{Constants.Rub}:{Constants.Dollar}"] = Constants.DivideMathOperation,
            [$"{Constants.Dollar}:{Constants.Rub}"] = Constants.MultiplyMathOperation,
            [$"{Constants.Rub}:{Constants.Euro}"] = Constants.DivideMathOperation,
            [$"{Constants.Euro}:{Constants.Rub}"] = Constants.MultiplyMathOperation,
            [$"{Constants.Dollar}:{Constants.Euro}"] = Constants.DivideMathOperation,
            [$"{Constants.Euro}:{Constants.Dollar}"] = Constants.MultiplyMathOperation
        };
        private readonly Dictionary<string, decimal> _ratesRelations;

        public CurrencyConverter(Dictionary<string, decimal> ratesRelations)
        {
            _ratesRelations = ratesRelations;
        }

        public decimal CalculateTotalValue(decimal partValue, string partCurrentCurrency, string convertTo, string finalCurrency)
        {
            decimal finalValue = partValue;
            var currentCurrency = partCurrentCurrency;
            if (!string.IsNullOrEmpty(convertTo))
            {
                if (currentCurrency == convertTo)
                {
                    throw new ArgumentException($"{Constants.WrongInputMessage}:Конвертация в ту же валюту");
                }

                finalValue = Calculate(finalValue, currentCurrency, convertTo);
                currentCurrency = convertTo;
            }

            if (currentCurrency != finalCurrency)
            {
                finalValue = Calculate(finalValue, currentCurrency, finalCurrency);
            }

            return finalValue;
        }


        private decimal Calculate(decimal firstValue, string currentCurrency, string convertTo)
        {
            var exchangePattern = $"{currentCurrency}:{convertTo}";
            var rate = _ratesRelations[exchangePattern];
            var mathOperation = _currencyMathOperations[exchangePattern];
            decimal finalValue;
            switch (mathOperation)
            {
                case Constants.MultiplyMathOperation:
                    finalValue = firstValue * rate;
                    break;
                case Constants.DivideMathOperation:
                    finalValue = firstValue / rate;
                    break;
                default:
                    throw new ArgumentException($"{Constants.WrongInputMessage}:Введен некорректный оператор");
            }
            return finalValue;
        }
    }
}
