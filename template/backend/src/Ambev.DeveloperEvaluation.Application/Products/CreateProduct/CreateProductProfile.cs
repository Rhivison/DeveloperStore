using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

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
            CreateMap<CreateProductCommand, Product>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new Domain.ValueObjects.ProductRating
            {
                Rate = src.Rating.Rate,
                Count = src.Rating.Count
            }));

            CreateMap<Product, CreateProductResult>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDto
                {
                    Rate = src.Rating.Rate,
                    Count = src.Rating.Count
                }));
        }
    }
}