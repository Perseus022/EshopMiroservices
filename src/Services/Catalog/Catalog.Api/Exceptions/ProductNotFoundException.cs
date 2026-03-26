namespace Catalog.Api.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(Guid productId)
            : base($"Product with Id: {productId} not found")
        {

        }
        public ProductNotFoundException(string SearchName)
            : base($"No Product found with this criteria {SearchName}")
        {

        }

    }
}
