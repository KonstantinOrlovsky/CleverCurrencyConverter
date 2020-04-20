

namespace DataRetrieval
{
   public interface IExtractData
    {
         string DataExtractionFromLocal(string expression);
         string DataExtractionFromUrl(string expression);
    }
}
