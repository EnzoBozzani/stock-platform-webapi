using api.Dtos.Stock;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByIdAsync(Guid id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(Guid id, UpdateStockDto stockDto);
        Task<Stock?> DeleteAsync(Guid id);
        Task<bool> StockExists(Guid id);
    }
}