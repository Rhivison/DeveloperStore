using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{   
    /// <summary>
    /// Profile for mapping between Application and API CreatePooduct responses
    /// </summary>
    public class CreateProductProfile: Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateProduct feature
        /// </summary>
        public CreateProductProfile()
        {   
            
             // 1. Primeiro crie o mapeamento para o RatingDto
            CreateMap<(decimal Rate, int Count), ProductRatingDto>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
                .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

            // 2. Mapeamento Request -> Command
            CreateMap<CreateProductRequest, CreateProductCommand>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => 
                    new ProductRatingDto { Rate = src.Rate, Count = src.Count }));

            // 3. Mapeamento RatingDto -> RatingResponse
            CreateMap<ProductRatingDto, ProductRatingResponse>()
                .ReverseMap(); // Se precisar do mapeamento inverso

            // 4. Mapeamento Result -> Response
            CreateMap<CreateProductResult, CreateProductResponse>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating));
        }
            
        
    }
}