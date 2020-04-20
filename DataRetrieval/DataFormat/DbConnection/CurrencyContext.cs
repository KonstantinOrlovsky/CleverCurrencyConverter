using System.Data.Entity;
using DataRetrieval.DataFormat;

namespace DataRetrieval
{
    class CurrencyContext : DbContext
    {
        public CurrencyContext() : base("DbConnection") { }
        public DbSet<ExchangeRate> DbExchangeRates{ get; set; }
    }
}
