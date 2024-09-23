using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ProductApi.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    }

    // Существующее свойство для таблицы продуктов
    public DbSet<Product> Products { get; set; }

    // Новое свойство для таблицы инвентаря (Inventory)
    public DbSet<Inventory> Inventory { get; set; }
  }

}