using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace APISimples.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options) { }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ConversaModel> Conversas {get; set;}
        public DbSet<MensagemModel> Mensagens { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new ConversaMap());
            modelBuilder.ApplyConfiguration(new MensagemMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
