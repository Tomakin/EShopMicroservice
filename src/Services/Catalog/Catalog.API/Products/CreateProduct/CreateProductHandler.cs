namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand
    (string Name, string Description, string ImageFile, List<string> Categories, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create product entity from command object
        var product = new Product()
        {
            Id = Guid.NewGuid(),
            Categories = command.Categories,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Name = command.Name,
            Price = command.Price
        };

        // Persist product
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return result

        return new CreateProductResult(product.Id);
    }
}

