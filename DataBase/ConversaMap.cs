using APISimples.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APISimples.DataBase
{
    public class ConversaMap : IEntityTypeConfiguration<ConversaModel>
    {
        public void Configure(EntityTypeBuilder<ConversaModel> builder)
        {
            builder.HasKey(x => x.id);
            builder.HasOne(c => c.user1).WithMany(u => u.conversasIniciadas).HasForeignKey(c => c.idUser1).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(c => c.user2).WithMany(u => u.conversasParticipadas).HasForeignKey(c => c.idUser2).OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
