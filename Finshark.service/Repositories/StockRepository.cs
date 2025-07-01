//using FinShark.api.Data;
using FinShark.Domain.Dtos.Stock;
using FinShark.Service.Stocks.Command.Update;
//using FinShark.api.Interfaces;
using FinShark.Domain.Repositories;
//using FinShark.api.Models;
using FinShark.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FinShark.api.Data;

namespace FinShark.Service.Repositories
{
    public class StockRepository : IStockRepository // For implementing Repository pattern
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);  
        }

        public async Task<Stock?> UpdateAsync(UpdateStockCommandDto updateDto)
        {
            var StockDomain = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == updateDto.Id);
            if (StockDomain == null)
            {
                return null;
            }
            StockDomain.Symbol = updateDto.Symbol;
            StockDomain.CompanyName = updateDto.CompanyName;
            StockDomain.Purchase = updateDto.Purchase;
            StockDomain.LastDiv = updateDto.LastDiv;
            StockDomain.Industry = updateDto.Industry;
            StockDomain.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();
            return StockDomain;
        }
    }
}
