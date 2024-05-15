using AllTrialsAndWalks.Models.Domain;
using AllTrialsAndWalks.Models.DTO;
using AllTrialsAndWalks.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllTrialsAndWalks.Controllers
{
    // https://localhost:portnumber/api/Regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;


        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // https://localhost:portnumber/api/Regions/
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            var regionDomain = await regionRepository.GetRegionsAsync();

            return Ok(mapper.Map<List<RegionDto>>(regionDomain));
        }

        //https://localhost:portnumber/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //https://localhost:portnumber/api/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionRequestDto region)
        {
            var regionDomain = mapper.Map<Region>(region);
            regionDomain = await regionRepository.CreateAsync(regionDomain);

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id}, regionDto);
        }

        //https://localhost:portnumber/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto region)
        {
            var regionDomain = mapper.Map<Region>(region);
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //https://localhost:portnumber/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var regionDomain = await regionRepository.DeleteAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
    }
}
 