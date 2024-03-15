using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
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

        public async Task<Stock?> DeleteAsync(Guid id)
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

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(s => s.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrEmpty(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(Guid id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(Guid id, UpdateStockDto updateStock)
        {
            var existingStock = await _context.Stocks.FindAsync(id);

            if (existingStock == null)
            {
                return null;
            }

            if (updateStock.Symbol != null)
            {
                existingStock.Symbol = updateStock.Symbol;
            }
            if (updateStock.CompanyName != null)
            {
                existingStock.CompanyName = updateStock.CompanyName;
            }
            if (updateStock.Industry != null)
            {
                existingStock.Industry = updateStock.Industry;
            }
            if (updateStock.Purchase != null)
            {
                existingStock.Purchase = (decimal)updateStock.Purchase;
            }
            if (updateStock.LastDiv != null)
            {
                existingStock.LastDiv = (decimal)updateStock.LastDiv;
            }
            if (updateStock.MarketCap != null)
            {
                existingStock.MarketCap = (long)updateStock.MarketCap;
            }

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<bool> StockExists(Guid id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }
    }
}