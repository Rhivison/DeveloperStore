using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Microsoft.AspNetCore.Authorization;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{   
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new cart (sale)
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            Console.WriteLine($"UserIdClaim: {userIdClaim?.Value}");
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value) || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized(new ApiResponse
                {
                    Success = false,
                    Message = "User is not authenticated"
                });
            }

            var command = _mapper.Map<CreateSaleCommand>(request);
            command.UserId = userId;

            var result = await _mediator.Send(command, cancellationToken);
            Console.WriteLine($"CreateSaleResult.UserId: {result.UserId}");
            var response = _mapper.Map<CreateSaleResponse>(result);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = response
            });
        }
    }
}
