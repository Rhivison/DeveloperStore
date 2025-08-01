using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductById;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetProductCategories;
using Microsoft.AspNetCore.Authorization;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ProductsController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request,CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);

            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<CreateProductResponse>(result);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = response
            });


        }

        /// <summary>
        /// Retrieves a PRoduct by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductByIdRequest { Id = id };
            var validator = new GetProductByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductByIdCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetProductByIdResponse>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = _mapper.Map<GetProductByIdResponse>(response)
            });
        }

        /// <summary>
        /// Deletes a product by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "User deleted successfully"
            });
        }

        /// <summary>
        /// Retrieves a paginated list of products
        /// </summary>
        /// <param name="request">Query parameters: page, size, order</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paginated list of products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromQuery] GetProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            var response = new GetProductResponse
            {
                Data = _mapper.Map<List<ProductItem>>(result.Data),
                TotalItems = result.TotalItems,
                CurrentPage = result.CurrentPage,
                TotalPages = result.TotalPages
            };
            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = response
            });

        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="request">Updated product data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var result = await _mediator.Send(command, cancellationToken);

            var response = _mapper.Map<UpdateProductResponse>(result);

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = response
            });
        }

        /// <summary>
        /// Retrieves products by category with optional pagination and ordering.
        /// </summary>
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductsByCategoryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCategoryAsync([FromRoute] string category, [FromQuery] GetProductsByCategoryRequest request)
        {
            

            var validator = new GetProductsByCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductsByCategoryCommand>(request);
            command.Category = category;

            var result = await _mediator.Send(command);

            var response = _mapper.Map<GetProductsByCategoryResponse>(result);

            return Ok(new ApiResponseWithData<GetProductsByCategoryResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = response
            });
        }

        /// <summary>
        /// Retrieves all product categories.
        /// </summary>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductCategoriesResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoriesAsync([FromQuery] GetProductCategoriesRequest request)
        {
            var command = _mapper.Map<GetProductCategoriesCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<GetProductCategoriesResponse>(result);

            return Ok(new ApiResponseWithData<GetProductCategoriesResponse>
            {
                Success = true,
                Message = "Product categories retrieved successfully",
                Data = response
            });
        }

    }
}