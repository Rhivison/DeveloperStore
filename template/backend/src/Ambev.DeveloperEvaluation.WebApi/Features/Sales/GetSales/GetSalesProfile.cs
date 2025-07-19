using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesProfile: Profile
    {
        public GetSalesProfile()
        {
             CreateMap<GetSalesRequest, GetSalesCommand>()
                .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.Page))
                .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Size))
                .ForMember(dest => dest.OrderBy, opt => opt.MapFrom(src => src.Order));

            CreateMap<GetSalesResult, GetSalesResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
                .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
            
             CreateMap<GetSalesResult, GetSalesResponse>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
                .ForMember(dest => dest.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
            
            CreateMap<GetSaleDto, SaleResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<GetSaleItemDto, SaleProductResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}