using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockClaimsPrincipal.Controllers;
using NSubstitute;
using System.Security.Claims;

namespace MockClaimsPrincipal.UnitTests;

public class ProductControllerTests
{
    private readonly Fixture _fixture = new();
    private readonly ProductController _controller;
    private readonly IProductService _productService = Substitute.For<IProductService>();

    public ProductControllerTests()
    {
        _controller = new ProductController(_productService);
        _controller.InitializeClaims();
    }

    [Fact]
    public async Task When_User_Is_An_Admin_Then_Returns_All_Products()
    {
        _controller.InitializeClaims(new Claim(ClaimTypes.Role, "admin"));

        var expectedProducts = _fixture.Create<Product[]>();
        _productService.GetAllProducts().Returns(Task.FromResult(expectedProducts));

        var actualProducts = await _controller.GetProducts();

        expectedProducts.Should().BeEquivalentTo(actualProducts);
    }

    [Fact]
    public async Task When_User_Is_Not_An_Admin_Then_Returns_Public_Products()
    {
        var expectedProducts = _fixture.Create<Product[]>();
        _productService.GetPublicProducts().Returns(Task.FromResult(expectedProducts));

        var actualProducts = await _controller.GetProducts();

        expectedProducts.Should().BeEquivalentTo(actualProducts);
    }
}

public static class TestExtensions
{
    public static void InitializeClaims(this Controller controller, params Claim[] claims)
    {
        controller.ControllerContext.HttpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(claims, "fake auth"))
        };
    }
}