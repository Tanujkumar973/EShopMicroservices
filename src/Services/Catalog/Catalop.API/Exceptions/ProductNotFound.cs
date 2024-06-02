namespace Catalog.API.Exceptions
{
    public class ProductNotFound : Exception
    {
        public ProductNotFound() : base("product not found")
        { }
    }
}
