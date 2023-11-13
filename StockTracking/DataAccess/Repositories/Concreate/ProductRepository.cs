using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Models.Dtos.ResponseDto;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Concreate;

public class ProductRepository : EfRepositoryBase<BaseDbContext, Product, Guid>, IProductRepository
{
    public ProductRepository(BaseDbContext context) : base(context)
    {
    }

    public List<ProductDetailDto> GetAllProductDetails()
    {
        var details = Context.Products

    }

    public List<ProductDetailDto> GetDetailsByCategoryId(int categoryId)
    {
        throw new NotImplementedException();
    }

    public ProductDetailDto GetProductDetails(int Id)
    {
        throw new NotImplementedException();
    }
}
