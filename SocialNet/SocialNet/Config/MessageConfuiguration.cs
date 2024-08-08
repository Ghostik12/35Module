using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNet.Models;

namespace SocialNet.Config
{
    public class MessageConfuiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(m => m.Sender)
           .WithMany()
           .HasForeignKey(m => m.SenderId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Recipient)
                   .WithMany()
                   .HasForeignKey(m => m.RecipientId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
