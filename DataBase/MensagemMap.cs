using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APISimples.DataBase
{
    public class MensagemMap : IEntityTypeConfiguration<MensagemModel>
    {
        public void Configure(EntityTypeBuilder<MensagemModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.conteudo).HasMaxLength(500);
            builder.HasOne(m => m.conversa).WithMany(c => c.mensagens).HasForeignKey(m => m.conversaId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.remetente).WithMany().HasForeignKey(m => m.remetenteId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
