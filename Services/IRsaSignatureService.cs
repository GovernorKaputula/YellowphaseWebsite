namespace YellowphaseWebsite.Services
{
    public interface IRsaSignatureService
    {
        string Sign(string payload);
        bool Verify(string payload, string signature);
    }
}
