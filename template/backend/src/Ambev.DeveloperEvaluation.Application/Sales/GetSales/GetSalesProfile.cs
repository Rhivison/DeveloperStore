using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{   
    /// <summary>
    /// AutoMapper profile for mapping Sale to GetSaleDto
    /// </summary>
    public class GetSalesProfile: Profile
    {
        public GetSalesProfile()
        {
            CreateMap<Sale, GetSaleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));
                
            CreateMap<SaleItem, GetSaleItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}