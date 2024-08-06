using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNet.Models;

namespace SocialNet.Config
{
    public class MessageConfuiguration
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Mesages").HasKey(p => p.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}
