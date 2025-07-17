using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductProfile: Profile
    {
        /// <summary>
        /// Profile for mapping between Application and API GetProduct responses
        /// </summary>
        public GetProductProfile()
        {
            CreateMap<GetProductRequest, GetProductCommand>();
            CreateMap<ProductDto, ProductItem>();
            CreateMap<GetProductsResult, GetProductResponse>();
        }
    }
}