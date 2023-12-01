﻿using AutoMapper;
using MassTransit;
using MongoDB.Entities;
using SearchService.Contracts;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;
        public AuctionCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine(" --> Consuming auction created: " + context.Message.Id);

            var item = _mapper.Map<Item>(context.Message);

            if (item.Model == "Foo") throw new ArgumentException("Cant sell cars with the name Foo");

            await item.SaveAsync();
        }
    }
}