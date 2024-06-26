using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }

    /* // Mapping btw different properties name
     public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDTO, UserDomain>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.FullName))
                .ReverseMap();
        }
    }

    public class UserDTO
    {
        public string FullName { get; set; }
    }

    public class UserDomain
    {
        public string Name { get; set; }
    }
     */



    /* // Mapping btw same properties name
     public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDTO, UserDomain>().ReverseMap(); // Reverse will also work
        }
    }

    public class UserDTO
    {
        public string FullName { get; set; }
    }

    public class UserDomain
    {
        public string FullName { get; set; }
    }
     */
}
