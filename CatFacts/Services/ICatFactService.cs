namespace CatFacts.Services
{
    public interface ICatFactService
    {
        Task<Dictionary<string, int>?> ReadAllFacts(string inputFile);
    }
}
