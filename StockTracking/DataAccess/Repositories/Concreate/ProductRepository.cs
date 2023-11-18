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
        var details = Context.Products.Join(
            Context.Categories,
            p => p.CategoryId,
            c => c.Id,
            (product, category) => new ProductDetailDto
            {
                Name = product.Name,
                CategoryName = category.Name,
                Id = product.Id,
                Price = product.Price,
                Stock = product.Stock
            }).ToList();
        return details;
    }

    public List<ProductDetailDto> GetDetailsByCategoryId(int categoryId)
    {
        var details = Context.Products.Where(c=>c.CategoryId == categoryId).Join(
           Context.Categories,
           p => p.CategoryId,
           c => c.Id,
           (product, category) => new ProductDetailDto
           {
               Name = product.Name,
               CategoryName = category.Name,
               Id = product.Id,
               Price = product.Price,
               Stock = product.Stock

           }).ToList();

        return details;

    }
    public ProductDetailDto GetProductDetails(Guid Id)
    {
        var details = Context.Products.Where(p => p.Id == Id).Select(
            product => new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName=product.Category.Name,
                Price = product.Price,
                Stock = product.Stock
            }).FirstOrDefault();
        return details;
            
            

            
    }


}