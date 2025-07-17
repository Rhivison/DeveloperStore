using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{   
    /// <summary>
    /// Profile for mapping between Application and API CreatePRoduct responses
    /// </summary>
    public class CreateProductProfile: Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProduct feature
        /// </summary>
        public CreateProductProfile()
        {   
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductCommand, CreateProductRequest>();
        }
    }
}