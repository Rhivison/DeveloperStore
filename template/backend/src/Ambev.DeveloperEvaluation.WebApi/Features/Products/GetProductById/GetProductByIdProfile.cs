using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdProfile: Profile
    {
        public GetProductByIdProfile()
        {   
            CreateMap<GetProductByIdRequest, GetProductByIdCommand>();
            CreateMap<GetProductByIdResult, GetProductByIdResponse>();
            CreateMap<ProductRatingDto, ProductRatingResponse>();
        }
    }
}