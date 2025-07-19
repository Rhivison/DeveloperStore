using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{   
    /// <summary>
    /// Profile for mapping between Product entity and CreateProductResponse
    /// </summary>
    public class CreateProductProfile: Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProduct operation
        /// </summary>
        public CreateProductProfile()
        {
            CreateMap<ProductRatingDto, ProductRating>();
            CreateMap<ProductRating, ProductRatingDto>();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, CreateProductResult>();
        }
    }
}