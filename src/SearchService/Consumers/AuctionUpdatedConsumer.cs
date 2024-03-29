using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;

namespace SearchService.Consumers
{
    public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
    {
        private readonly IMapper _mapper;

        public AuctionUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine($" --> Consuming AuctionUpdated event:{context.Message.Id}");
            var item = _mapper.Map<Item>(context.Message);

            var result = await DB.Update<Item>()
                .Match(i => i.ID == context.Message.Id)
                .ModifyOnly(x => new Item
                {
                    Color = x.Color,
                    Make = x.Make,
                    Model = x.Model,
                    Year = x.Year,
                    Mileage = x.Mileage,
                }, item)
                .ExecuteAsync();

            if (!result.IsAcknowledged)
            {
                throw new MessageException(typeof(AuctionUpdated), "Problem updating mongo db.");
            }
        }
    }
}