using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSaleByIdHandlerTests
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IMapper _mapper;
        private readonly GetSaleByIdHandler _handler;

        public GetSaleByIdHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GetSaleByIdProfile>(); // Configure isso no seu projeto
            });

            _mapper = config.CreateMapper();
            _handler = new GetSaleByIdHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Should return sale by id successfully")]
        public async Task Handle_ShouldReturnSale_WhenValidId()
        {
            var saleId = Guid.NewGuid();
            var command = new GetSaleByIdCommand { Id = saleId };

            var fakeSale = new Sale
            {
                Id = saleId,
                SaleNumber = "S123",
            };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                .Returns(fakeSale);

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(saleId);
        }

        [Fact(DisplayName = "Should throw validation exception when ID is empty")]
        public async Task Handle_ShouldThrowValidationException_WhenIdIsEmpty()
        {
            var command = new GetSaleByIdCommand { Id = Guid.Empty };

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Should throw key not found when sale doesn't exist")]
        public async Task Handle_ShouldThrowKeyNotFound_WhenSaleNotFound()
        {
            var saleId = Guid.NewGuid();
            var command = new GetSaleByIdCommand { Id = saleId };

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                .Returns((Sale?)null);

            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Sale with ID {saleId} was not found.");
        }
    }
}
