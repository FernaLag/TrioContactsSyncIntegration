using Moq;
using Xunit;
using Shouldly;
using Trio.ContactSync.Application.Features.MockApi.Handlers;
using Trio.ContactSync.Application.Features.MockApi.Queries;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Domain.Contracts.Clients;

namespace Trio.ContactSync.Application.UnitTests.UnitTests.MockApi
{
    public class GetMockApiContactsRequestHandlerTests
    {
        private readonly Mock<IMockApiClient> _mockApiClient;
        private readonly GetMockApiContactsRequestHandler _handler;

        public GetMockApiContactsRequestHandlerTests()
        {
            _mockApiClient = new();
            _handler = new GetMockApiContactsRequestHandler(_mockApiClient.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnContacts()
        {
            // Arrange
            List<Contact> contacts = [new(), new()];
            _mockApiClient.Setup(x => x.GetAsync()).ReturnsAsync(contacts);

            // Act
            List<Contact> result = await _handler.Handle(new GetMockApiContactsRequest(), CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBe(contacts);
            _mockApiClient.Verify(x => x.GetAsync(), Times.Once);
        }
    }
}