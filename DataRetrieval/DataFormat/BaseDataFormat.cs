using DataRetrieval.ExtractData;
using DataRetrieval.SaveLog;

namespace DataRetrieval.DataFormat
{
    public abstract class BaseDataFormat
    {
        protected IExtractData ExtractData;
        protected IExtractFromDB ExtractDataFromDb;
        protected ISave Save;

        internal virtual string DataExtractionFromLocal(string expression)
        {
            return ExtractData.DataExtractionFromLocal(expression);
        }

        internal virtual string DataExtractionFromUrl(string expression)
        {
            return ExtractData.DataExtractionFromUrl(expression);
        }

        internal virtual string ExtractFromDb(string expression)
        {
            return ExtractDataFromDb.ExtractFromDB(expression);
        }
        internal abstract void SaveToLog(string expression, string result);
    }
}
