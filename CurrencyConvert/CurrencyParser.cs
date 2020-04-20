using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CurrencyConvert
{
    public sealed class CurrencyParser
    {
        private readonly Dictionary<string, string> _currencyRelations = new Dictionary<string, string>
        {
            [Constants.Dollar] = "ToDollar",
            [Constants.Euro] = "ToEuro",
            [Constants.Rub] = "ToRub"
        };

        public string ParseFinalCurrencyOperation(string conversionOperation)
        {
            conversionOperation = conversionOperation.Trim();
            var relation = ParseCurrencyOperation(conversionOperation);
            if (relation.Key == null)
            {
                throw new ArgumentException($"{Constants.WrongInputMessage}:Отсутствие данных при окончательной конвертации");
            }
            var currency = new Regex($"^{relation.Value}:$");
            var match = currency.Match(conversionOperation);
            if (!match.Success)
            {
                throw new ArgumentException($"{Constants.WrongInputMessage}:Некорректный ввод валюты окончательной конвертации");
            }

            return relation.Key;
        }

        public string ParseCurrencyConversionOperation(string conversionOperation)
        {
            conversionOperation = conversionOperation.Trim();
            var relation = ParseCurrencyOperation(conversionOperation);
            if (relation.Key == null)
            {
                return "";
            }
            var currency = new Regex($":{relation.Value}$");
            var match = currency.Match(conversionOperation);
            if (!match.Success)
            {
                throw new ArgumentException($"{Constants.WrongInputMessage}:Некорректный ввод валюты окончательной конвертации");
            }

            return relation.Key;
        }

        private KeyValuePair<string, string> ParseCurrencyOperation(string conversionOperation)
        {
            var relationsExists = _currencyRelations.Any(pair => conversionOperation.Contains(pair.Value));
            if (!relationsExists)
            {
                return new KeyValuePair<string, string>();
            }
            KeyValuePair<string, string> relation = _currencyRelations.FirstOrDefault(pair => conversionOperation.Contains(pair.Value));

            return relation;
        }
    }
}
