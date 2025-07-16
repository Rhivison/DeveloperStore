using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductsProfile: Profile
    {
        public GetProductsProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDto
                {
                    Rate = src.Rating.Rate,
                    Count = src.Rating.Count
                }));
        }
    }
}