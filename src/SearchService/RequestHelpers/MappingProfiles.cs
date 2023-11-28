using AutoMapper;
using SearchService.Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<AuctionCreated, Item>(); 
            CreateMap<AuctionUpdated, Item>();         
        }
    }
}
