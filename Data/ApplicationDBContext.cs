using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        //usado para acessar toda a tabela de Stocks
        public DbSet<Stock> Stocks { get; set; }
        //e de Comments
        public DbSet<Comment> Comments { get; set; }
    }
}