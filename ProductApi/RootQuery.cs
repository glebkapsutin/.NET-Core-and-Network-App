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

        Field<ListGraphType<ProductType>>(
            "products",
            resolve: context => _context.Products.ToListAsync() // Используем контекст для доступа к данным
        );

        Field<ListGraphType<InventoryType>>(
            "inventory",
            resolve: context => _context.Inventory.ToListAsync() // Используем контекст для доступа к данным
        );
    }
}
