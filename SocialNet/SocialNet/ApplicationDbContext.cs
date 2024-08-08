using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNet.Config;
using SocialNet.Models;

namespace SocialNet
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base (options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FriendConfiguration());
            builder.ApplyConfiguration(new MessageConfuiguration());
        }
    }
}
