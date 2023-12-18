using Azure;
using CoreCrossCuttingConcerns.Exceptions;
using DataAccess.Repositories.Abstract;
using Models.Dtos.RequestDto;
using Models.Dtos.ResponseDto;
using Models.Entities;
using Moq;
using Service.BusinessRules;
using Service.BusinessRules.Abstract;
using Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.UnitTest.Products;

public class ProductServiceTest
{
    private ProductService _service;
    private Mock<IProductRepository> _mockRepository;
    private Mock<IProductRules> _mockRules;

    private ProductAddRequest productAddRequest;
    private ProductUpdateRequest productUpdateRequest;
    private Product product;
    private ProductResponseDto productResponseDto;



    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IProductRepository>();
        _mockRules = new Mock<IProductRules>();
        _service = new ProductService(_mockRepository.Object, _mockRules.Object);
        productAddRequest = new ProductAddRequest(Name: "test", Stock: 25, Price: 2500, CategoryId: 1);
        productUpdateRequest = new ProductUpdateRequest(Id: new Guid(), Name: "test", Stock: 25, Price: 2500, CategoryId: 1);
        product = new Product
        {
            Id = new Guid(),
            Name = "test",
            CategoryId = 1,
            Price = 2500,
            Stock = 25,
            Category = new Category() { Id = 1, Name = "teknoloji", Products = new List<Product>() { new Product() } }
        };
        productResponseDto = new ProductResponseDto(Id: new Guid(), Name: "test", Stock: 25, Price: 2500, CategoryId: 1);
        

    }

    [Test]
    public void Add_WhenProductNameIsUnique_ReturnsOk()
    {
        //Arrange
        _mockRules.Setup(x => x.ProductNameMustBeUnique(productAddRequest.Name));
        _mockRepository.Setup(x => x.Add(product));

        //Act
        var result = _service.Add(productAddRequest);

        //Assert
        Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        Assert.AreEqual(result.Data, productResponseDto);
        Assert.AreEqual(result.Message, "Ürün Eklendi");

    }

    [Test]
    public void Add_WhenProductNameIsNotUnique_ReturnsBadRequest()
    {
        //Arrenge
        _mockRules.Setup(x => x.ProductNameMustBeUnique(productAddRequest.Name))
            .Throws(new BusinessException("Ürün ismi benzersiz olmalı"));
        //Act
        var result = _service.Add(productAddRequest);

        //Assert
        Assert.AreEqual(result.Message, "Ürün ismi benzersiz olmalı");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Delete_WhenProductIsPresent_ReturnsOk()
    {
        //Arrange
        Guid Id = new Guid();
        _mockRules.Setup(x => x.ProductIsPresent(Id));
        _mockRepository.Setup(x => x.GetById(Id, null)).Returns(product);
        _mockRepository.Setup(x => x.Delete(product));

        //Act
        var result = _service.Delete(Id);

        //Assert
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(result.Data, productResponseDto);
        Assert.AreEqual(result.Message, "Ürün Silindi");

    }

    [Test]
    public void Delete_WhenProductIsNotPresent_ReturnsBadRequest()
    {
        //Arrenge
        Guid Id = new Guid();
        _mockRules.Setup(x => x.ProductIsPresent(Id))
            .Throws(new BusinessException($"Id si :{Id} olan ürün bulunamadı.")
        );

        //Act
        var result = _service.Delete(Id);

        //Assert
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
        Assert.AreEqual(result.Message, $"Id si :{Id} olan ürün bulunamadı.");
        Assert.AreNotEqual(result.Data, productResponseDto);
    }

    [Test]
    public void GetAll_ReturnOk()
    {
        //Arrange
        var products = new List<Product>()
        {
            product    
        };

        var response = new List<ProductResponseDto>()
        { 
            productResponseDto     
        };
        _mockRepository.Setup (x => x.GetAll(null,null)).Returns(products);

        //Act
        var result = _service.GetAll();

        //Assert
        Assert.AreEqual (result.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(result.Data, response);

    }

    [Test]
    public void GetByDetailId_WhenDetailIsPresent_ReturnsOk()
    {
        //Arrange
        ProductDetailDto dto = new ProductDetailDto()
        {
            CategoryName = "Test",
            Id = Guid.NewGuid(),
            Name = "Test",
            Price = 1000,
            Stock = 25
        };

        /*
        var response = new Response<ProductDetailDto>()
        {
            StatusCode = System.Net.HttpStatusCode.OK,
            Data = dto
        };
       */

        _mockRules.Setup(x => x.ProductIsPresent(It.IsAny<Guid>()));
        _mockRepository.Setup(x => x.GetProductDetails(It.IsAny<Guid>())).Returns(dto);

        //Act
        var result = _service.GetByDetailId(It.IsAny<Guid>());

        //Assert 
        Assert.AreEqual(result.Data, dto);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        
    }

    [Test]
    public void GetByDetailId_WhenDetailIsNotPresent_BadRequest()
    {
        //Arrange
        _mockRules.Setup(x=>x.ProductIsPresent(It.IsAny<Guid>())).Throws(
           new BusinessException($"Id si :{It.IsAny<Guid>()} olan ürün bulunamadı."));

        //Act
        var result = _service.GetByDetailId(It.IsAny<Guid>());

        //Assert
        Assert.AreEqual(result.Message, $"Id si :{It.IsAny<Guid>()} olan ürün bulunamadı.");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void GetById_WhenIsPresent_ReturnsOk()
    {
        //Arrange
        Guid Id = new Guid();
        _mockRules.Setup(x => x.ProductIsPresent(Id));
        _mockRepository.Setup(x => x.GetById(Id, null)).Returns(product);

        //Act
        var result = _service.GetById(Id);

        //Assert
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        Assert.AreEqual(result.Data, productResponseDto);
    }

    [Test]
    public void GetById_WhenIsNotPresent_BadRequest()
    {
        //Arrange 
        Guid Id = new Guid();
        _mockRules.Setup(x=>x.ProductIsPresent(Id)).Throws(new BusinessException("Test"));

        //Act
        var result = _service.GetById(Id);

        //Assert
        Assert.AreEqual(result.Message,"Test");
        Assert.AreEqual(result.StatusCode, HttpStatusCode.BadRequest);
    }

    [Test]
    public void Update_WhenProductIsUniqe_ReturnsOk()
    {
        //Arrange
        _mockRules.Setup(x => x.ProductNameMustBeUnique(productUpdateRequest.Name));
        _mockRepository.Setup(x => x.Update(product));

        //Act
        var result = _service.Update(productUpdateRequest);

        //Assert
        Assert.AreEqual(result.Data, productResponseDto);
        Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
    }

    [Test]
    public void Update_WhenProductIsNotUniqe_ReturnsBadRequest()
    {
        //Arrange
        _mockRules.Setup(x=>x.ProductNameMustBeUnique(productUpdateRequest.Name)).
            Throws(new BusinessException("Ürün ismi benzersiz olmalı"));

        //Act
        var result = _service.Update(productUpdateRequest);

        //Assert
        Assert.AreEqual(result.Message, "Ürün ismi benzersiz olmalı");
        Assert.AreEqual(result.StatusCode,HttpStatusCode.BadRequest);
    }

   /*
    [Test]
    public void GetAllDetails_ReturnsOk()
    {
        //Arrange
        _mockRepository.Setup(x => x.GetAllProductDetails());

        //Act
        var result = _service.GetAllDetails();

        //Assert 
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<ProductDetailDto>>(result.Data);
        Assert.AreEqual(result.StatusCode,HttpStatusCode.OK);
    }

  */
}
