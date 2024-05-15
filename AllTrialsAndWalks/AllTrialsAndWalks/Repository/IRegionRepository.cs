using AllTrialsAndWalks.Models.Domain;

namespace AllTrialsAndWalks.Repository
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegionsAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}
