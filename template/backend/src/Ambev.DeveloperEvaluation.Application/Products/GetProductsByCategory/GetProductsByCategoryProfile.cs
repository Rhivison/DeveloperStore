using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class GetProductsByCategoryProfile: Profile
    {
        public GetProductsByCategoryProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRatingDto
            {
                Rate = src.Rating.Rate,
                Count = src.Rating.Count
            }));

            CreateMap<(IEnumerable<Product> Products, int TotalItems, int CurrentPage, int TotalPages), GetProductsByCategoryResult>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
                .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
        }
        
    }
}