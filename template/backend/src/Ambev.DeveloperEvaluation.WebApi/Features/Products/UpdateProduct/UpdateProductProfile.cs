using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductProfile: Profile
    {
        public UpdateProductProfile()
        {
            
            
            CreateMap<UpdateProductRequest, UpdateProductCommand>();

            CreateMap<ProductRatingRequest, ProductRatingDto>();

            
            CreateMap<UpdateProductResult, UpdateProductResponse>();

            CreateMap<ProductRatingDto, ProductRatingResponse>();
           
        }
    }
}