using CsvHelper;
using System.Globalization;

namespace JaniceChat.FinBot
{
    public interface IStockParser
    {
        IList<Stock> Parse(Stream stream);
    }

    public class StockParser : IStockParser
    {
        public IList<Stock> Parse(Stream stream) 
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Stock>().ToList();
        }
    }
}