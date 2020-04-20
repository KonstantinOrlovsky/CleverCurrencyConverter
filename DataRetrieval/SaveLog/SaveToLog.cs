using System;
using System.Configuration;
using System.IO;

namespace DataRetrieval.SaveLog
{
    class SaveToLog : ISave
    {
        private const string LogFilePathKey = "LogFilePath";
        private readonly string _logFilePath = ConfigurationManager.AppSettings.Get(LogFilePathKey);
        private DateTime GetData()
        {
            var dataNow = DateTime.Now;

            return dataNow;
        }

        public void Save(string sourceExpression, string result, string dataFormat)
        {
            var finalText = $"Номер операции: {Guid.NewGuid()} Входное выражение: {sourceExpression} Данные получены из {dataFormat} Результат: {result} Дата события: {GetData()}";
            File.AppendAllText(_logFilePath, finalText + Environment.NewLine);
        }
    }
}
