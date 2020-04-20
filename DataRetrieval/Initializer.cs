using DataRetrieval.DataFormat;
using DataRetrieval.DataFormat.DbConnection;

namespace DataRetrieval
{
    class Initializer
    {
        string _expression;
        BaseDataFormat _baseDataFormat;

        public Initializer(string expression)
        {
            _expression = expression;
        }

       internal string InitializingFromJson(string methodOfExtraction)
        {
            _baseDataFormat = new Json();

            return  Constants.Url == methodOfExtraction ? (new Json().DataExtractionFromUrl(_expression)) : (new Json().DataExtractionFromLocal(_expression));
        }

        internal string InitializingFromXml(string methodOfExtraction)
        {
            _baseDataFormat = new Xml();

            return Constants.Url == methodOfExtraction ? (new Xml().DataExtractionFromUrl(_expression)) : (new Xml().DataExtractionFromLocal(_expression));
        }

        internal string InitializingFromDataBase()
        {
            _baseDataFormat = new DataBase();

           return _baseDataFormat.ExtractFromDb(_expression);
        }

        public void SaveToLog(string result)
        {
            _baseDataFormat.SaveToLog(_expression, result);
        }
    }
}
