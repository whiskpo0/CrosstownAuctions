using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly IMapper _mapper;
        private readonly AuctionDbContext _context;
        public AuctionRepository(IMapper mapper, AuctionDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddAuction(Auction auction)
        {
            _context.Auctions.Add(auction);
        }

        public async Task<AuctionDto> GetAuctionByIdAsync(Guid Id)
        {
            return await _context.Auctions
                .ProjectTo<AuctionDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Auction> GetAuctionEntityByIdAsync(Guid Id)
        {
            return await _context.Auctions
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == Id); 
        }

        public async Task<List<AuctionDto>> GetAuctionsAsync(string date)
        {
            var query = _context.Auctions.OrderBy(x => x.Item.Make).AsQueryable(); 

            if(!string.IsNullOrEmpty(date))
            {
                query = query.Where(x => x.UpdatedAt.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0); 
            }

            return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public void RemoveAuction(Auction auction)
        {
            _context.Auctions.Remove(auction);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
