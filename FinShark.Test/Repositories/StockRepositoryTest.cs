using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinShark.api.Data;
using FinShark.Domain.Entities;
using FinShark.Service.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;


namespace FinShark.Service.Test.Repositories
{
    public class StockRepositoryTest
    {
        private ApplicationDBContext GetDbContextWithData() // This method sets up an in-memory database with test data for Stock entities
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()  
                .UseInMemoryDatabase(databaseName: "StockDB_test")
                .Options;
            var context = new ApplicationDBContext(options);

            context.Stocks.AddRange(
                new Stock { Id = 1, CompanyName = "Apple", Symbol = "AAPL", Industry = "Tech", Purchase = 100, LastDiv = 5, MarketCap = 1000 },
                new Stock { Id = 2, CompanyName = "Microsoft", Symbol = "MSFT", Industry = "Tech", Purchase = 150, LastDiv = 4, MarketCap = 900 }
            );
            context.SaveChanges();
            return context;
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllStocks()
        {
            var context = GetDbContextWithData();
            var repository = new StockRepository(context);
            var result = await repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectStock()
        {
            var context = GetDbContextWithData();
            var repository = new StockRepository(context);
            var result = await repository.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Apple", result.CompanyName);
        }

       

    }
}
