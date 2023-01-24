namespace JaniceChat.FinBot
{
    public interface IStockGateway
    {
        Task<IList<Stock>> GetStock(string stockCode);
    }

    public class StockGateway : IStockGateway
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IStockParser _stockParser;

        public StockGateway(IHttpClientFactory httpClientFactory, IStockParser stockParser)
        {
            _httpClientFactory = httpClientFactory;
            _stockParser = stockParser;
        }

        public async Task<IList<Stock>> GetStock(string stockCode)
        {
            string csvUrl = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";
            using var client = _httpClientFactory.CreateClient();

            using var response = await client.GetAsync(csvUrl);

            return _stockParser.Parse(await response.Content.ReadAsStreamAsync());
        }
    }
}