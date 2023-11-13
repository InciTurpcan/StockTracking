using Core.Persistence.Repositories;
using Models.Entities;

namespace DataAccess.Repositories.Abstract;

public interface ICategoryRepository : IEntityRepository<Category,int>
{
}
