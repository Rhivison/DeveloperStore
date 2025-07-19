using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleProfile: Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<Sale, UpdateSaleResult>()
            .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src => src.SaleDate))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items.Where(i => !i.Cancelled)));

            CreateMap<SaleItem, UpdateSaleItemResultDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}