using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class CreateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
        private readonly IEventPublisher _eventPublisher = Substitute.For<IEventPublisher>();
        private readonly IMapper _mapper;
        private readonly CreateSaleCommandHandler _handler;

        public CreateSaleHandlerTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CreateSaleProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateSaleCommandHandler(_saleRepository, _productRepository, _mapper, _eventPublisher);
        }

        [Fact(DisplayName = "Should create sale successfully")]
        public async Task Handle_ShouldCreateSale_WhenValidCommand()
        {
            
            var command = CreateSaleHandlerTestData.DataGenerator.GenerateValidCommand();
            command.CustomerId = Guid.NewGuid(); 
            command.BranchId = Guid.NewGuid();

            var productId = command.Items[0].ProductId;
            var fakeProduct = CreateSaleHandlerTestData.DataGenerator.GenerateProduct(productId);

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>())
                .Returns(fakeProduct);

            _saleRepository.AddAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(callInfo => Task.FromResult(callInfo.Arg<Sale>()));

            
            var result = await _handler.Handle(command, CancellationToken.None);

            
            result.Should().NotBeNull();
            result.SaleNumber.Should().Be(command.SaleNumber);
            result.Items.Should().HaveCount(command.Items.Count);
        }

        [Fact(DisplayName = "Should throw validation exception when command is invalid")]
        public async Task Handle_ShouldThrow_WhenCommandInvalid()
        {
        
            var command = new CreateSaleCommand(); 

           
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

           
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Should throw exception when product not found")]
        public async Task Handle_ShouldThrow_WhenProductNotFound()
        {
          
            var command = CreateSaleHandlerTestData.DataGenerator.GenerateValidCommand();
            command.CustomerId = Guid.NewGuid();
            command.BranchId = Guid.NewGuid();

            var missingProductId = command.Items[0].ProductId;

            _productRepository.GetByIdAsync(missingProductId, Arg.Any<CancellationToken>())
                .Returns((Product?)null);

           
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

           
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Product with ID {missingProductId} not found");
        }
    }
}
