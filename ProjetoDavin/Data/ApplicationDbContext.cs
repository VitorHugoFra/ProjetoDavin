using Microsoft.EntityFrameworkCore;
using ProjetoDavin.Models;

namespace ProjetoDavin.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Contato> Contatos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
  }
}
