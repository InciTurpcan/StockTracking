using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Models.Entities;

namespace DataAccess.Repositories.Concreate;

public class CategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) : base(context)
    {
    }
}
