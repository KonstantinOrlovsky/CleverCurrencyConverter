using DataRetrieval.ExtractData;
using DataRetrieval.SaveLog;

namespace DataRetrieval.DataFormat
{
    internal class Json:BaseDataFormat
    {
        public Json()
        {
            ExtractData = new ExtractFromJson();
        }

        internal override void SaveToLog(string expression,string result)
        {
            new SaveToLog().Save(expression,result,Constants.Json);
        }
    }
}
