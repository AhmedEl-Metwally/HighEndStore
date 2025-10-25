namespace Domain.Contracts.SeedData
{
    public interface IDataSeeding
    {
        Task SeedDataAsync();
        Task SeedIdentityDataAsync();   
    }
}
