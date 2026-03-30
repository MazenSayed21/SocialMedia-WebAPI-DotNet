using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SOCIALIZE.Models;
namespace SOCIALIZE.Data
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
      
        public AppDbContext(DbContextOptions<AppDbContext>options) :base(options){ }

        public DbSet<AppUser> users { set; get; }
        public DbSet<Post> posts { set; get; }

        public DbSet<Comment> comments { set; get; }

        public DbSet<Reaction>reactions { set; get; } 

        public DbSet<FriendShip>friendShips { set; get; }

        public DbSet<Notification>notifications { set; get; }

    }
}
