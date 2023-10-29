using ContractsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractsManager.Services
{
    public class DBContext : DbContext
    {
        public static string ConnectionString { get; set; }

        public DBContext() 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        public virtual DbSet<Contract> contracts { get; set; }
        public virtual DbSet<ContractStage> contractstages { get; set; }
    }
}
