using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNet.Models;

namespace SocialNet.Config
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend> 
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("UserFriends").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.CurrentFriend)
                .WithMany()
                .HasForeignKey(f => f.CurrentFriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
