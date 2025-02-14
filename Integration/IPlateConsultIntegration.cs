namespace PlacasAPI.Integration
{
    public interface IPlateConsultIntegration
    {
        Task<Dictionary<string, string>> GetDataFromTheWebsite(string plate);
    }
}
