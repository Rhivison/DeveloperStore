using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{   
    /// <summary>
    /// Profile for mapping between Product entity and ProductDto
    /// </summary>
    public class GetProductsProfile: Profile
    {
        public GetProductsProfile()
        {   
            /// <summary>
            /// Initializes the mappings for GetProduct operation
            /// </summary>
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDto
                {
                    Rate = src.Rating.Rate,
                    Count = src.Rating.Count
                }));
        }
    }
}