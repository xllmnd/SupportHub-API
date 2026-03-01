using Moq;
using SupportHub.Application.Features.Customers.Commands.CreateCustomer;
using SupportHub.Application.Features.Tickets.Commands.CreateTicket;
using SupportHub.Application.Interfaces.Persistence;
using SupportHub.Domain.Entities;
using Xunit;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<IRepository<Customer>> _mockRepo;
    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTests()
    {
        _mockRepo = new Mock<IRepository<Customer>>();

        _handler = new CreateCustomerCommandHandler(_mockRepo.Object);
    }

    [Fact] 
    public async Task Handle_ValidCustomer_ShouldReturnCustomerId()
    {
        var command = new CreateCustomerCommand("Ahmet Yılmaz", "ahmet@test.com");

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Customer>()))
                 .ReturnsAsync(new Customer { Id = 1, Name = "Ahmet Yılmaz" });

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(1, result); 
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once); // Add metodu tam 1 kez çalışmalı
    }

    [Fact]
    public async Task Handle_InvalidCustomer_ShouldThrowException()
    {
        var mockCustomerRepo = new Mock<IRepository<Customer>>();
        var mockTicketRepo = new Mock<IRepository<Ticket>>();

        mockCustomerRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Customer)null);

        var handler = new CreateTicketCommandHandler(mockTicketRepo.Object, mockCustomerRepo.Object);
        var command = new CreateTicketCommand(99, "Error", "Description", SupportHub.Domain.Enums.TicketCategory.Billing, SupportHub.Domain.Enums.TicketPriority.Low);

        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
    }
}