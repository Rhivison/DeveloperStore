using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdProfile: Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<Sale, GetSaleByIdResult>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate));
            
            CreateMap<SaleItem, GetSaleItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}