using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNet.Models;

namespace SocialNet
{
    public class ApplicationDbContext : IdentityDbContext <User>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base (options) 
        {
            Database.EnsureCreated();
        }
    }
}
