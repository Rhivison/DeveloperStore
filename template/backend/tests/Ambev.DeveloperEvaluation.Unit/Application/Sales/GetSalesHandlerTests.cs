using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class GetSalesHandlerTests
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IMapper _mapper;
        private readonly GetSalesHandler _handler;

        public GetSalesHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Sale, GetSaleDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetSalesHandler(_saleRepository, _mapper);
        }

        [Fact(DisplayName = "Should return paginated sales with default ordering")]
        public async Task Handle_ShouldReturnPaginatedSales_WhenValidRequest()
        {
            // Arrange
            var sales = GetSalesTestData.CreateSales(5);
            _saleRepository.GetAllAsync(Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(sales));

            var command = GetSalesTestData.CreateValidCommand(page: 1, size: 10);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().HaveCount(sales.Count);
            result.TotalItems.Should().Be(sales.Count);
            result.CurrentPage.Should().Be(1);
            result.TotalPages.Should().Be(1);
            result.Data.Select(d => d.Id).Should().BeEquivalentTo(sales.Select(s => s.Id));
        }
        
        [Fact(DisplayName = "Should throw ValidationException when request is invalid")]
        public async Task Handle_ShouldThrowValidationException_WhenInvalidRequest()
        {
            // Arrange
            var command = GetSalesTestData.CreateValidCommand(page: 0, size: 0);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
