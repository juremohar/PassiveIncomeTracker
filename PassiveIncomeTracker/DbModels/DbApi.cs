using Microsoft.EntityFrameworkCore;

namespace PassiveIncomeTracker.DbModels
{
    public class DbApi : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
       
        }

        public DbApi(DbContextOptions<DbApi> options) : base(options)
        {
        }

        public DbSet<TUser> Users {  get; set; }
        public DbSet<TCryptocurrency> Cryptocurrencies { get; set; }
        public DbSet<TUserInterest> UsersInterests { get;set; } 
        public DbSet<TUserRealizedInterest> UsersRealizedInterests { get;set; } 
        public DbSet<TSession> Sessions { get;set; } 
        public DbSet<TCodeInterestPayout> InterestPayouts { get; set; }
        public DbSet<TService> Services { get; set; }
    }
}
