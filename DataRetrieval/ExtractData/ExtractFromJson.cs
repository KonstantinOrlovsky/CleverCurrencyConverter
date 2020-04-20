using CurrencyConvert;
using DataRetrieval.JsonClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text.Json;

namespace DataRetrieval.ExtractData
{
    class ExtractFromJson:IExtractData
    {
        private const string Path = "Resources /CurrensyJSON.json";
        private const string UrlJsonPath = "UrlJsonPath";
        private RootObject _rootReceivedData;
        private string _expression;
       
        private Dictionary<string, decimal> FillRatesRelations()
        {
            var ratesRelations = new Dictionary<string, decimal>
            {
                [$"{Constants.Rub}:{Constants.Dollar}"] = ParseCurrency(_rootReceivedData.Valute.USD.Value),
                [$"{Constants.Dollar}:{Constants.Rub}"] = ParseCurrency(_rootReceivedData.Valute.USD.Value),
                [$"{Constants.Rub}:{Constants.Euro}"] = ParseCurrency(_rootReceivedData.Valute.EUR.Value),
                [$"{Constants.Euro}:{Constants.Rub}"] = ParseCurrency(_rootReceivedData.Valute.EUR.Value),
                [$"{Constants.Dollar}:{Constants.Euro}"] = decimal.Divide(ParseCurrency(_rootReceivedData.Valute.EUR.Value), ParseCurrency(_rootReceivedData.Valute.USD.Value)),
                [$"{Constants.Euro}:{Constants.Dollar}"] = decimal.Divide(ParseCurrency(_rootReceivedData.Valute.EUR.Value), ParseCurrency(_rootReceivedData.Valute.USD.Value))
            };
            return ratesRelations;
        }
        private decimal ParseCurrency(double value) => (decimal)value;
        public string FinalDataExtraction(string dataExtracted)
        {
            var rootReceivedData = JsonSerializer.Deserialize<RootObject>(dataExtracted);
            _rootReceivedData = rootReceivedData;

            return new Calculator(FillRatesRelations()).GetResultFromExpression(_expression).ToString();
        }

        public string  DataExtractionFromLocal(string expression)
        {
            _expression = expression;
            var dataFromLocal = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}{Path}");

            return FinalDataExtraction(dataFromLocal);
        }

        public string DataExtractionFromUrl(string expression)
        {
            _expression = expression;
            var urlJsonPath = ConfigurationManager.AppSettings.Get(UrlJsonPath);
            var dataUrl = WebRequest.Create(urlJsonPath);
            var dataStreamReader = new StreamReader(dataUrl.GetResponse().GetResponseStream());
            var dataFromUrl = dataStreamReader.ReadToEnd();
            dataStreamReader.Dispose();

            return FinalDataExtraction(dataFromUrl);
        }
    }
}
