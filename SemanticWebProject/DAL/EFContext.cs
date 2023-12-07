using Microsoft.EntityFrameworkCore;
using SemanticWebProject.DAL.Entities;

namespace SemanticWebProject.DAL;

public class EFContext : DbContext
{
    public EFContext(DbContextOptions<EFContext> options)
        : base(options)
    {
    }
    public DbSet<University> Universities { get; set; }
}