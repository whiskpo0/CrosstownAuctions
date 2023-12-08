using AutoMapper;
using Contracts;
using BiddingService.DTOs;
using BiddingService.Models;

namespace BiddingService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bid, BidDto>();
            CreateMap<Bid, BidPlaced>();
        }
    }
}
