using AllTrialsAndWalks.Models.Domain;
using AllTrialsAndWalks.Models.DTO;
using AllTrialsAndWalks.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllTrialsAndWalks.Controllers
{
    //https:localhost:portnumber/api/Walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(IWalksRepository walksRepositry, IMapper mapper)
        {
            this.walksRepository = walksRepositry;
            this.mapper = mapper;
        }

        //https:localhost:portnumber/api/Walks?filterOn=name&filterBy=type
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterBy, [FromQuery] string? SortBy, [FromQuery] bool? isAscending,int pageNumber, int pagesize)
        {
            var walksDomain = await walksRepository.GetWalksAsync(filterOn,filterBy,SortBy,isAscending ?? true,pageNumber,pagesize);

            return Ok(mapper.Map<List<WalksDto>>(walksDomain));
        }

        //https:localhost:portnumber/api/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetWalksById([FromRoute] Guid id)
        {
            var walkDomain = await walksRepository.GetWalkByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalksDto>(walkDomain));
        }

        //https:localhost:portnumber/api/Walks
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWalkDto walk)
        {
            var walkDomain = mapper.Map<Walk>(walk);

            walkDomain = await walksRepository.CreateAsync(walkDomain);

            var walkDto = mapper.Map<WalksDto>(walkDomain);
            return CreatedAtAction(nameof(Walk), walkDto);
        }

        //https:localhost:portnumber/api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateWalkRequestDto walkRequestDto)
        {
            var walkDomain = mapper.Map<Walk>(walkRequestDto);

            walkDomain = await walksRepository.UpdateAsync(id, walkDomain);

            return Ok(mapper.Map<WalksDto>(walkDomain));
        }

        //https:localhost:portnumber/api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var walk = await walksRepository.DeleteAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Walk>(walk));
        }
    }
}
