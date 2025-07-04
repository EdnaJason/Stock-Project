using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinShark.Domain.Repositories;
using FinShark.Domain.Entities;
using FinShark.Service.Stocks.Query.GetAll;
using Microsoft.Extensions.Logging;
using Moq;
using FinShark.Domain.Dtos.Stock;

namespace FinShark.Service.Test.Handlers.Stocks.Query
{
    public class GetAllStockQueryHandlerTest
    {

        [Fact]

        public async Task Handle_ReturnsAllStockDtos()
        {
            var mockRepo = new Mock<IStockRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<GetAllStockQueryHandler>>();

            var fakeStocks = new List<Stock>
            {
                 new Stock { Id = 1, CompanyName = "Apple", Symbol = "AAPL", Industry = "Tech", Purchase = 100, LastDiv = 5, MarketCap = 1000 },
                 new Stock { Id = 2, CompanyName = "Microsoft", Symbol = "MSFT", Industry = "Tech", Purchase = 150, LastDiv = 4, MarketCap = 900 }
            };
            var fakeDtos = new List<StockDto>
            {
                new StockDto { Id = 1, CompanyName = "Apple", Symbol = "AAPL", Industry = "Tech", Purchase = 100, LastDiv = 5, MarketCap = 1000 },
                new StockDto { Id = 2, CompanyName = "Microsoft", Symbol = "MSFT", Industry = "Tech", Purchase = 150, LastDiv = 4, MarketCap = 900 }
            };

            mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeStocks);
            mockMapper.Setup(mapper => mapper.Map<List<StockDto>>(fakeStocks)).Returns(fakeDtos);

            var handler = new GetAllStockQueryHandler(
                mockRepo.Object,
                mockMapper.Object,
                mockLogger.Object
                );

            var result = await handler.Handle(new GetAllStockQuery(), CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("MSFT", result[1].Symbol);

        }
    }
}
