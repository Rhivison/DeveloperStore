using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdProfile: Profile
    {
        public GetProductByIdProfile()
        {   
            CreateMap<GetProductByIdRequest, GetProductByIdCommand>();
            CreateMap<ProductDto, GetProductByIdResponse>();
        }
    }
}