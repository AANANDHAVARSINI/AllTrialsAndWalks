using AllTrialsAndWalks.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace AllTrialsAndWalks.Repository
{
    public interface IWalksRepository
    {
        Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterBy = null, string? SortBy = null, bool isAscending=true, int pageNumber = 1, int pageSize = 3);
        Task<Walk> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task <Walk> DeleteAsync(Guid id);
    }
}
