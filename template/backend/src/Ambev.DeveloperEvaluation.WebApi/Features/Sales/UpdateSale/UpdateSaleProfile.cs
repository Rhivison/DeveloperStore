using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile: Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateSaleProductRequest, UpdateSaleItemDto>();

            CreateMap<UpdateSaleResult, UpdateSaleResponse>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Items));

            CreateMap<UpdateSaleItemResultDto, UpdateSaleProductResponse>();
        }
    }
}