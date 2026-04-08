using StockApp.Models;
using StockApp.Interfaces;
using StockApp.Data;

namespace StockApp.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context; // ✅ FIXED
        }

        public async Task SaveAsync(StockData stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }
    }
}