using CurrencyConvert;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace DataRetrieval.ExtractData
{
    class ExtractFromXml : IExtractData
    {
        private const string PathFromLocal = "Resources/ExchangeRate.xml";
        private const string PathFromUrl = "UrlXmlPath";
        private const string IdDollar = "R01235";
        private const string IdEuro = "R01239";
        private XmlElement _dataXmlElement;
        private string _expression;

        private Dictionary<string, decimal> FillRatesRelations()
        {
            var ratesRelations = new Dictionary<string, decimal>
            {
                [$"{Constants.Rub}:{Constants.Dollar}"] = ExtractValueCurrency(IdDollar),
                [$"{Constants.Dollar}:{Constants.Rub}"] = ExtractValueCurrency(IdDollar),
                [$"{Constants.Rub}:{Constants.Euro}"] = ExtractValueCurrency(IdEuro),
                [$"{Constants.Euro}:{Constants.Rub}"] = ExtractValueCurrency(IdEuro),
                [$"{Constants.Dollar}:{Constants.Euro}"] = decimal.Divide(ExtractValueCurrency(IdEuro), ExtractValueCurrency(IdDollar)),
                [$"{Constants.Euro}:{Constants.Dollar}"] = decimal.Divide(ExtractValueCurrency(IdEuro), ExtractValueCurrency(IdDollar))
            };
            return ratesRelations;
        }
        private XmlNode GetAttribute(XmlNode xml) => xml.Attributes.GetNamedItem("ID");
        private decimal ExtractValueCurrency(string currency)
        {
            foreach (XmlNode dataXmlNode in _dataXmlElement)
            {
                if (GetAttribute(dataXmlNode).Value == currency)
                {
                    foreach (XmlNode dataXmlChildnode in dataXmlNode)
                    {
                        if (dataXmlChildnode.Name == "Value")
                        {
                            return decimal.Parse(dataXmlChildnode.InnerText);
                        }
                    }
                }

            }

            throw new ArgumentException("Отсутствие необходимых данных в используемом XML документе ");
        }

        string DataExtraction(string path)
        {
            XmlDocument dataXmlDocument = new XmlDocument();
            dataXmlDocument.Load(path);
            _dataXmlElement = dataXmlDocument.DocumentElement;

            return new Calculator(FillRatesRelations()).GetResultFromExpression(_expression).ToString();
        }

        public string DataExtractionFromLocal(string expression)
        {
            _expression = expression;
            var pathFromLocal = $"{AppDomain.CurrentDomain.BaseDirectory}{PathFromLocal}";

            return DataExtraction(pathFromLocal);
        }

        public string DataExtractionFromUrl(string expression)
        {
            _expression = expression;
            var pathFromUrl = ConfigurationManager.AppSettings.Get(PathFromUrl);

            return DataExtraction(pathFromUrl);
        }
    }
}
