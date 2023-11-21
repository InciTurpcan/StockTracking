using CoreCrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.BusinessRules;

public class ProductRules
{
    private readonly IProductRepository _productRepository;

    public ProductRules(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void ProductNameMustBeUnique(string productName)
    {
        var product = _productRepository.GetByFilter(x=>x.Name==productName,null);

        if (product is not null)
        {
            throw new BusinessException("Ürün ismi benzersiz olmalı");
        }
    }

    public void ProductIsPresent(Guid Id) 
    { 
        var product = _productRepository.GetById(Id);

        if (product is  null)
        {
            throw new BusinessException($"Id si :{Id} olan ürün bulunamadı.");
        }
    }
}
