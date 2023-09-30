using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace Infra.Context
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(DbContextOptions<DbContextBase>options) : base(options)
        {
        }

        public DbSet<Categoria>? Categoria { set; get; }
        public DbSet<Produto>? Produto { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string mySqlConnection = "Server=localhost;DataBase=CitelBD;Uid=root;Pwd=Fdas*2018;SslMode=none";
            optionsBuilder.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)); ;
        }
    }
}