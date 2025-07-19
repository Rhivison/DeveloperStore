using Ambev.DeveloperEvaluation.Application.DTOs;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using System;
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{   

    /// <summary>
    /// Handles the retrieval of a specific sale by ID
    /// </summary>
    public class GetSaleByIdHandler:  IRequestHandler<GetSaleByIdCommand, GetSaleByIdResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleByIdResult> Handle(GetSaleByIdCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSaleByIdCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale == null)
                throw new KeyNotFoundException($"Sale with ID {request.Id} was not found.");

            var result = _mapper.Map<GetSaleByIdResult>(sale);
            return result;
        }
    }
}