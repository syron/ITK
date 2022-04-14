using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KIT.Services;

public class IdentitiesDbContext : IdentityDbContext
{
    public IdentitiesDbContext(DbContextOptions<IdentitiesDbContext> options)
        : base(options)
    {
    }
}

