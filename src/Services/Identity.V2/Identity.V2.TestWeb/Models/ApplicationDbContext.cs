using Microsoft.EntityFrameworkCore;

namespace Identity.V2.TestWeb.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}