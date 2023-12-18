
using Azure;
using WebAPI.Controllers;

namespace WebAPI.UnitTest;

public class BaseControllerTests
{
    private BaseController _controller;

    [SetUp]
    public void Setup()
    { 
        _controller = new BaseController();
    }

    [Test]
    public void ActionResultInstance_WhenStatusCodeOk_ReturnsOk()
    {
        //Arrange
        var response = new Response<string>()
        {

        };
    }
}
