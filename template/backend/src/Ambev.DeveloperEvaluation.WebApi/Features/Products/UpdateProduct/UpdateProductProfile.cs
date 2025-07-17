using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile: Profile
    {
        public UpdateProductProfile()
        {
            
            CreateMap<UpdateProductRequest, UpdateProductCommand>();

            
            CreateMap<ProductRating, ProductRatingDto>().ReverseMap();

            
            CreateMap<Product, ProductDto>().ReverseMap();

            
            CreateMap<ProductDto, UpdateProductResponse>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

            CreateMap<UpdateProductResult, UpdateProductResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Product.Category))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Product.Image))
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Product.Rating.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Product.Rating.Count));
           
        }
    }
}