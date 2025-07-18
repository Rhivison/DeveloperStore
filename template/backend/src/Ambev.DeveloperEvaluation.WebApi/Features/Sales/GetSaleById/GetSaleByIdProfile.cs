using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.DTOs;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdProfile: Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<GetSaleByIdRequest, GetSaleByIdCommand>();

            CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
            CreateMap<GetSaleItemDto, SaleItemResponse>();
        }
    }
}