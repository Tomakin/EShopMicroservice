namespace Catalog.API.Products.CreateProduct;

public record CreateProductRequest(string Name, string Description, string ImageFile, List<string> Categories, decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products",
            async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);
            return Results.Created($"products/{result.Id}", result);
        })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create new product")
            .WithDescription("Create new product");
    }
}

