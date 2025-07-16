using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductProfile: Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductCommand, Product>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new Domain.ValueObjects.ProductRating
                {
                    Rate = src.Rate,
                    Count = src.Count
                }));

            CreateMap<Product, ProductDto>();
        }
    }
}