using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APISimples.DataBase
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        void IEntityTypeConfiguration<UsuarioModel>.Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.username).IsRequired().HasMaxLength(255);
            builder.Property(x => x.password).IsRequired().HasMaxLength(255);
        }
    }
}
