namespace Catalog.Api.Catalog.Producrs.CreateProduct
{
    public record CreateProductCommand(
        string Name, List<string> Category, string Description, string ImageFile, decimal Price
        ) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            // Here you would typically save the product to a database
            // For this example, we'll just return the created product's ID
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
