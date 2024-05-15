using AllTrialsAndWalks.Models.Domain;
using AllTrialsAndWalks.Models.DTO;
using AutoMapper;

namespace AllTrialsAndWalks.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        { 
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<UpdateRegionRequestDto,Region>().ReverseMap();
            CreateMap<CreateRegionRequestDto,Region>().ReverseMap();
            CreateMap<WalksDto,Walk>().ReverseMap();
            CreateMap<AddWalkDto,Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto,Walk>().ReverseMap();
        }
    }
}
