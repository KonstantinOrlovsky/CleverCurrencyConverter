using DataRetrieval.ExtractData;
using DataRetrieval.SaveLog;

namespace DataRetrieval.DataFormat
{
    internal class Xml:BaseDataFormat
    {
        public Xml()
        {
            ExtractData = new ExtractFromXml();
        }

        internal override void SaveToLog(string expression, string result)
        {
            new SaveToLog().Save(expression, result, Constants.Xml);
        }
    }
}
