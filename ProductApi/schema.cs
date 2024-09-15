using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

public class Schema : global::GraphQL.Types.Schema
{
    public Schema(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Query = serviceProvider.GetRequiredService<RootQuery>(); // Передаем RootQuery
    }
}
