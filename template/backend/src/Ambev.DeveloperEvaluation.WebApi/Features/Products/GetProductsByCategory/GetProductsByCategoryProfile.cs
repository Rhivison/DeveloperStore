using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryProfile: Profile
    {
        public GetProductsByCategoryProfile()
        {
            CreateMap<GetProductsByCategoryRequest, GetProductsByCategoryCommand>()
                .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.OrderBy, opt => opt.MapFrom(src => src.OrderBy));

            CreateMap<GetProductsByCategoryResult, GetProductsByCategoryResponse>();
            CreateMap<ProductDto, ProductByCategoryResponseItem>();
            CreateMap<ProductRatingDto, ProductRatingResponse>();
        }
    }
}