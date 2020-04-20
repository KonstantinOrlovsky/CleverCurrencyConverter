using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CurrencyConvert
{
    internal sealed class PartsParser
    {
        private readonly string _part;

        public PartsParser(string part)
        {
            _part = part;
        }

        public decimal GetValue()
        {
            var numberRegex = new Regex(@"\d+(\.\d+)?");
            var match = numberRegex.Match(_part);
            if (match.Success)
            {
                return Convert.ToDecimal(match.Value);
            }
            else
            {
                throw new ArgumentException($"{Constants.WrongInputMessage}:Некорректное числовое значение валюты");
            }
        }

        public string GetCurrency()
        {
            List<string> currencyPatterns = new List<string>() {  $"[{Constants.Dollar}]", $"{Constants.Rub}", $"{Constants.Euro}" };
            var parsedCurrency = "";
            currencyPatterns.ForEach(currency =>
            {
                var regexCurrency = new Regex(currency);
                var match = regexCurrency.Match(_part);
                if (match.Success)
                {
                    parsedCurrency = match.Value;
                }
            });

            return parsedCurrency;
        }
    }
}
