using CoreCrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BusinessRules;

public class CategoryRules
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryRules(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void CategoryNameMustBeUnique(string categoryName)
    {
        var category = _categoryRepository.GetByFilter(x=>x.Name==categoryName);
        if (category != null) 
        {
            throw new BusinessException("Kategori adı benzersiz olmalı.");
        }
    }

    public void CategoryIsPresent(int Id)
    {
        var category = _categoryRepository.GetById(Id);
        if (category == null) 
        {
            throw new BusinessException($"Id: {Id} olan kategori bulunamadı.");
        }
    }
}
