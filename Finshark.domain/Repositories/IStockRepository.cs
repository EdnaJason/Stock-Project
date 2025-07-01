
using FinShark.Domain.Dtos.Stock;
using FinShark.Domain.Entities;

namespace FinShark.Domain.Repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task<Stock?> GetByIdAsync(int id);  // ? because tis has a possibility of returning a null value
        Task<Stock> CreateAsync(Stock stockDto);
        Task<Stock?> UpdateAsync(UpdateStockCommandDto updateDto);
        Task<Stock?> DeleteAsync(int id);
    }
}
 