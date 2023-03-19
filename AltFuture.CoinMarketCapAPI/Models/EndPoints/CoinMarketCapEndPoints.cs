namespace AltFuture.CoinMarketCapAPI.Models.EndPoints
{
    public class CoinMarketCapEndPoints
    {
        public V1 V1 { get; set; }
        public V2 V2 { get; set; }

        public int RateLimitHours { get; set; } = 0;
    }

}
