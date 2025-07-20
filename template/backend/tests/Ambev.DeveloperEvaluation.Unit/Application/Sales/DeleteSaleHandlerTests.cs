using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using NSubstitute;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.ORM.Mapping;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
namespace Ambev.DeveloperEvaluation.Unit.Application.Sales
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository = Substitute.For<ISaleRepository>();
        private readonly IEventPublisher _eventPublisher = Substitute.For<IEventPublisher>();
        private readonly IMapper _mapper;
        private readonly DeleteSaleCommandHandler _handler;

        public DeleteSaleHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<DeleteSaleProfile>());
            _mapper = config.CreateMapper();
            _handler = new DeleteSaleCommandHandler(_saleRepository, _eventPublisher);
        }

        [Fact(DisplayName = "Should cancel sale and items successfully")]
        public async Task Handle_ShouldCancelSaleAndItems_WhenValidCommand()
        {
            var saleId = Guid.NewGuid();
            uint xmin = 123;
            var command = DeleteSaleHandlerTestData.GenerateValidCommand(saleId, xmin);
            var sale = DeleteSaleHandlerTestData.GenerateSale(saleId, xmin);

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                .Returns(sale);

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns(callInfo => callInfo.Arg<Sale>());

            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Cancelled.Should().Be(true);
            sale.Cancelled.Should().BeTrue();
            sale.Items.Should().OnlyContain(i => i.Cancelled);
        }

        [Fact(DisplayName = "Should throw KeyNotFoundException when sale does not exist")]
         public async Task Handle_ShouldThrow_WhenXminMismatch()
        {
            var saleId = Guid.NewGuid();
            var sale = DeleteSaleHandlerTestData.GenerateSale(saleId, xmin: 321, 1); 
            var command = DeleteSaleHandlerTestData.GenerateValidCommand(saleId, xmin: 123);

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                .Returns(sale);
            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<DbUpdateConcurrencyException>()
                .Where(e => e.Message.Contains("modified by another process"));
        }

        [Fact(DisplayName = "Should throw InvalidOperationException when update fails")]
        public async Task Handle_ShouldThrow_WhenUpdateFails()
        {
            var saleId = Guid.NewGuid();
            uint xmin = 123;
            var command = DeleteSaleHandlerTestData.GenerateValidCommand(saleId, xmin);
            var sale = DeleteSaleHandlerTestData.GenerateSale(saleId, xmin);

            _saleRepository.GetByIdAsync(saleId, Arg.Any<CancellationToken>())
                .Returns(sale);

            _saleRepository.UpdateAsync(Arg.Any<Sale>(), Arg.Any<CancellationToken>())
                .Returns((Sale)null); // Simula falha no update

            var act = async () => await _handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Failed to cancel sale.");
        }
    }
}