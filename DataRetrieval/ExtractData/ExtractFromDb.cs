using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyConvert;
using DataRetrieval.DataFormat;

namespace DataRetrieval.ExtractData
{
    class ExtractFromDb:IExtractFromDB
    {
        private Dictionary<string, decimal> FillRatesRelations()
        {
            using (CurrencyContext db = new CurrencyContext())
            {
                //ExchangeRate exchangeRate = new ExchangeRate() { Dollar = 73.71m, Euro = 80.67m, DollarToEuro = 1.06m };

                //db.DbExchangeRates.Add(exchangeRate);
                //db.SaveChanges();

                ExchangeRate exchangeRates = db.DbExchangeRates.FirstOrDefault();
                if (exchangeRates != null)
                {
                    var ratesRelations = new Dictionary<string, decimal>
                    {
                        [$"{Constants.Rub}:{Constants.Dollar}"] = exchangeRates.Dollar,
                        [$"{Constants.Dollar}:{Constants.Rub}"] = exchangeRates.Dollar,
                        [$"{Constants.Rub}:{Constants.Euro}"] = exchangeRates.Euro,
                        [$"{Constants.Euro}:{Constants.Rub}"] = exchangeRates.Euro,
                        [$"{Constants.Dollar}:{Constants.Euro}"] = exchangeRates.DollarToEuro,
                        [$"{Constants.Euro}:{Constants.Dollar}"] = exchangeRates.DollarToEuro
                    };

                    return ratesRelations;
                }
            }
            throw  new ArgumentException("Отсутствуют данные в базе данных");
        }
       
        public string ExtractFromDB(string expression)
        {
            return new Calculator(FillRatesRelations()).GetResultFromExpression(expression).ToString();
        }
    }
}
