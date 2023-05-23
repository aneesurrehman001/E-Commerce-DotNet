using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams) : base(x =>
            /* 
            if brandId and typeId both are null it will return true. But, base will return false.
            if brandId and typeId has value but doesn't exist in database it will return false. Will not go in base class either.
            if brandId and typeId has value and also exist in database then it will return true. Base will return true as well.
            */
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId))
        {

        }

    }

}