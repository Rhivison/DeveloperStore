using AutoMapper;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{   
    /// <summary>
    /// Profile for mapping between User entity and GetUserResponse
    /// </summary>
    public class GetProductByIdProfile: Profile
    {   
        /// <summary>
        /// Initializes the mappings for Product operation
        /// </summary>
        public GetProductByIdProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}