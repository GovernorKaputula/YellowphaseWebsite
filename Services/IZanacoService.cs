namespace YellowphaseWebsite.Services
{
    public interface IZanacoService
    {
        Task<string> GetAccessTokenAsync();

        Task<bool> NameLookupAsync(
            string mobileNumber,
            string mobileNetwork);

        Task<bool> ChargeRequestAsync(
            decimal amount,
            string mobileNumber,
            string mobileNetwork,
            string transactionReference);
    }
}
