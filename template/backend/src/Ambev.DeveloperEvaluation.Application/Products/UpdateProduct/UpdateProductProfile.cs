using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile: Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    new ProductRating
                    {
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    }));
            CreateMap<Product, UpdateProductResult>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    new ProductRatingDto
                    {
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    }));

            // Entity → Result
            CreateMap<Product, UpdateProductResult>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src =>
                    new ProductRatingDto
                    {
                        Rate = src.Rating.Rate,
                        Count = src.Rating.Count
                    }));

            // (Auxiliary)
            CreateMap<ProductRatingDto, ProductRating>();
            CreateMap<ProductRating, ProductRatingDto>();
        }
    }
}