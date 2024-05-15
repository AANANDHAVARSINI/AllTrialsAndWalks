using AllTrialsAndWalks.Data;
using AllTrialsAndWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllTrialsAndWalks.Repository
{
    public class WalksRepository : IWalksRepository
    {
        private readonly AllTrialsDbcontext dbcontext;

        public WalksRepository(AllTrialsDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbcontext.Walks.AddAsync(walk);
            await dbcontext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var isExistWalk = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (isExistWalk != null)
            {
                return null;
            }

            dbcontext.Walks.Remove(isExistWalk);
            await dbcontext.SaveChangesAsync();
            return isExistWalk;
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await dbcontext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterBy = null, string? SortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 3)
        {
            var walks = dbcontext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //Filtering
            if(string.IsNullOrWhiteSpace(filterOn) && string.IsNullOrWhiteSpace(filterBy)) {
                if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterOn));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(SortBy))
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=> x.Name):walks.OrderByDescending(x => x.Name);
                }
            }

            //Pagination
            var results = (pageNumber - 1) * pageSize;
            return await walks.Skip(results).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var isExistWalk = await dbcontext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (isExistWalk != null)
            {
                return null;
            }

            isExistWalk.Name = walk.Name;
            isExistWalk.Description = walk.Description;
            isExistWalk.DifficultyId = walk.DifficultyId;
            isExistWalk.RegionId = walk.RegionId;
            isExistWalk.LengthInKm = walk.LengthInKm;
            isExistWalk.WalkImageUrl = walk.WalkImageUrl;

            await dbcontext.SaveChangesAsync(); 
            return isExistWalk;
        }
    }
}
