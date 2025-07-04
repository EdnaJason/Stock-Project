using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Entities;
using FinShark.Domain.Repositories;
using FinShark.Service.Stocks.Query.GetById;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace FinShark.Service.Test.Handlers.Stocks.Query
{
    public class GetByIdStockQueryHandlerTest
    {
        [Fact]
        public async Task Handle_ReturnsCorrectStock()
        {
            var mockRepo = new Mock<IStockRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<GetByIdStockQueryHandler>>();

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

            mockRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(fakeStocks[0]);
            mockMapper.Setup(mapper => mapper.Map<StockDto>(fakeStocks[0])).Returns(fakeDtos[0]);

            var handler = new GetByIdStockQueryHandler(
                mockRepo.Object,
                mockMapper.Object,
                mockLogger.Object
                );

            var result = await handler.Handle(new GetByIdStockQuery { Id = 1 }, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Apple", result.CompanyName);
        
        }

        [Fact]

        public async Task Handle_ThrowsKeyNotFoundExcpetion_WhenStockNotFounnd()
        {
            var mockRepo = new Mock<IStockRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<GetByIdStockQueryHandler>>();

            var stockId = 999;

            mockRepo.Setup(repo => repo.GetByIdAsync(stockId)).ReturnsAsync((Stock)null);

            var handler = new GetByIdStockQueryHandler(
                mockRepo.Object,
                mockMapper.Object,
                mockLogger.Object
                );

            var result = new GetByIdStockQuery{ Id = stockId};

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await handler.Handle(result, CancellationToken.None));
        }

    }
}
