using GraphQL.Types;
using ProductApi.Models;

public class ProductType : ObjectGraphType<Product>
{
    public ProductType()
    {
        Field(x => x.Id);
        Field(x => x.Name);
        Field(x => x.Price);
    }
}

public class InventoryType : ObjectGraphType<Inventory>
{
    public InventoryType()
    {
        Field(x => x.Id_Inventory);
        Field(x => x.Id_Product);
        Field(x => x.StockQantity);
    }
}
