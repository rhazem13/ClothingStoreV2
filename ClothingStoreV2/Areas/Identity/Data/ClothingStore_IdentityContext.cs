using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ClothingStoreV2.Areas.Identity.Data;
public class ClothingStore_IdentityContext : IdentityDbContext<IdentityUser>
{
    public ClothingStore_IdentityContext(DbContextOptions<ClothingStore_IdentityContext> options)
        : base(options)
    {
    }
    protected ClothingStore_IdentityContext(DbContextOptions options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
