using BuildingBlocks.Exceptions;

namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product not found", productId)
        {

        }
        public ProductNotFoundException(string SearchName)
            : base($"No Product found with this criteria {SearchName}")
        {

        }

    }
}
