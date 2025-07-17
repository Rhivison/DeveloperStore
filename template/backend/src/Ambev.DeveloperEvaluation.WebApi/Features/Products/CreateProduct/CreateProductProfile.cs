using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{   
    /// <summary>
    /// Profile for mapping between Application and API CreatePooduct responses
    /// </summary>
    public class CreateProductProfile: Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProduct feature
        /// </summary>
        public CreateProductProfile()
        {   
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<ProductRatingRequest, ProductRatingDto>();

            CreateMap<CreateProductResult, CreateProductResponse>();
            CreateMap<ProductRatingDto, ProductRatingResponse>();
            
        }
    }
}