using Moq;
using Xunit;
using Trio.ContactSync.Application.Features.Mailchimp.Handlers;
using Trio.ContactSync.Domain;
using Trio.ContactSync.Domain.Contracts.Clients;

namespace Trio.ContactSync.Application.UnitTests.UnitTests.Mailchimp
{
    public class UpdateMailchimpMembersCommandHandlerTests
    {
        private readonly Mock<IMailchimpClient> _mailchimpClientMock;
        private readonly UpdateMailchimpMembersCommandHandler _handler;

        public UpdateMailchimpMembersCommandHandlerTests()
        {
            _mailchimpClientMock = new();
            _handler = new UpdateMailchimpMembersCommandHandler(_mailchimpClientMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCallPostBatchAsync()
        {
            // Arrange
            List<MailchimpMember> mailchimpMembers = [new(), new()];

            _mailchimpClientMock.Setup(x => x.PostBatchAsync(mailchimpMembers))
                .ReturnsAsync(It.IsAny<HttpResponseMessage>);

            // Act
            await _handler.Handle(new UpdateMailchimpMembersCommand(mailchimpMembers), CancellationToken.None);

            // Assert
            _mailchimpClientMock.Verify(x => x.PostBatchAsync(mailchimpMembers), Times.Once);
        }
    }
}
