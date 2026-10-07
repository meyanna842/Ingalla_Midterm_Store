using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ingalla_Midterm_Store.Models;

namespace Ingalla_Midterm_Store.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
}
