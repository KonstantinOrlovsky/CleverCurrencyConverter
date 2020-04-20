using DataRetrieval.ExtractData;
using DataRetrieval.SaveLog;

namespace DataRetrieval.DataFormat.DbConnection
{
    class DataBase:BaseDataFormat
    {
        internal override string ExtractFromDb(string expression)
        {
            return new ExtractFromDb().ExtractFromDB(expression);
        }

        internal override void SaveToLog(string expression, string result)
        {
            new SaveToLog().Save(expression, result, Constants.DataBase);
        }
    }
}
