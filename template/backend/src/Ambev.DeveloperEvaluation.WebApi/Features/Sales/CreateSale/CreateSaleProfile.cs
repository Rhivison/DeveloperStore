using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile: Profile
    {
        public CreateSaleProfile()
        {   
            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id))
                .ForMember(dest => dest.BranchId, opt => opt.MapFrom(src => src.Branch.Id))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleItemRequest, CreateSaleItemDto>();

            CreateMap<CreateSaleResult, CreateSaleResponse>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items)); // <- certifique-se que `Items` exista

            CreateMap<SaleItemResult, ProductResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

             CreateMap<CreateSaleResult, CreateSaleResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<SaleItemResult, ProductResponse>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}