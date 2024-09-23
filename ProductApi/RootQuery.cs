using GraphQL.Types;
using ProductApi.Models;
using ProductApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class RootQuery : ObjectGraphType
{
    private readonly ApplicationDbContext _context;

    public RootQuery(ApplicationDbContext context)
    {
        _context = context;

        FieldAsync<ListGraphType<ProductType>>(
            "products",
            resolve: async context => await _context.Products.ToListAsync() // Используем контекст для доступа к данным
        );

        FieldAsync<ListGraphType<InventoryType>>(
            "inventory",
            resolve: async context => await _context.Inventory.ToListAsync() // Используем контекст для доступа к данным
        );
    }
}

