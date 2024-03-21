using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
    }
}