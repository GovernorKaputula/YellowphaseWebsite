namespace YellowphaseWebsite.Services
{
    public interface IExchangeRateService
    {
        Task<decimal> GetRateAsync(string currency);
    }
}