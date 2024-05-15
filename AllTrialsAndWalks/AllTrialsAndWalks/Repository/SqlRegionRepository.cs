using AllTrialsAndWalks.Data;
using AllTrialsAndWalks.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AllTrialsAndWalks.Repository
{
    public class SqlRegionRepository: IRegionRepository
    {
        private readonly AllTrialsDbcontext dbcontext;

        public SqlRegionRepository(AllTrialsDbcontext dbcontext) 
        {
            this.dbcontext = dbcontext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbcontext.Regions.AddAsync(region);
            await dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbcontext.Regions.Remove(existingRegion);
            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var region = await this.dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            return region;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            var regions = await dbcontext.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
