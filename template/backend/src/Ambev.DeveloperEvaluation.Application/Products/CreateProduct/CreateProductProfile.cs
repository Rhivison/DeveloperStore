using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{   
    /// <summary>
    /// Profile for mapping between User entity and CreateProductResponse
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
                Rate = src.Rate,
                Count = src.Count
            }));
        }
    }
}