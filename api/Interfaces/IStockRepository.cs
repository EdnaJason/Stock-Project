using FinShark.api.Dtos.Stock;
using FinShark.api.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinShark.api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);  // ? because tis has a possibility of returning a null value
        Task<Stock> CreateAsync(Stock stockDto);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
 