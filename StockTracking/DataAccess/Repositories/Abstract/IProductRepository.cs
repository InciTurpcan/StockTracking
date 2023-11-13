using Core.Persistence.Repositories;
using Models.Dtos.ResponseDto;
using Models.Entities;

namespace DataAccess.Repositories.Abstract;

public interface IProductRepository : IEntityRepository<Product,Guid>
{
    List<ProductDetailDto> GetAllProductDetails();
    List<ProductDetailDto> GetDetailsByCategoryId(int categoryId);
    ProductDetailDto GetProductDetails(int Id);
}
