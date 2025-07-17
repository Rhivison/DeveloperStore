using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories
{
    public class GetProductCategoriesProfile: Profile
    {
        public GetProductCategoriesProfile()
        {
            CreateMap<GetProductCategoriesRequest, GetProductCategoriesCommand>();
            CreateMap<GetProductCategoriesResult, GetProductCategoriesResponse>();
        }
    }
}