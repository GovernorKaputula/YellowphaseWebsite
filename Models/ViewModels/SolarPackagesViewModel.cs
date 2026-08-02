namespace YellowphaseWebsite.Models.ViewModels
{
    public class SolarPackagesViewModel
    {
        public List<(Product Product, List<string> Images)> ResidentialPackages { get; set; } = new();
        public List<(Product Product, List<string> Images)> CommercialPackages { get; set; } = new();
    }
}
