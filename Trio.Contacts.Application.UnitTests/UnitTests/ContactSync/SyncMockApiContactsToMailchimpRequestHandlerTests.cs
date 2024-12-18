using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using Trio.ContactSync.Application.Features.ContactSync.Handlers;
using Trio.ContactSync.Application.Features.ContactSync.Queries;
using Trio.ContactSync.Application.Features.Mailchimp.Commands;
using Trio.ContactSync.Application.Features.MockApi.Queries;
using Trio.ContactSync.Application.UnitTests.UnitTests.Mocks;
using Trio.ContactSync.Domain;
using Xunit;

namespace Trio.ContactSync.Application.UnitTests.UnitTests.ContactSync
{
    public class SyncMockApiContactsToMailchimpRequestHandlerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly SyncMockApiContactsToMailchimpRequestHandler _handler;

        public SyncMockApiContactsToMailchimpRequestHandlerTests()
        {
            _mediatorMock = ContactSyncMock.GetMediator();
            _mapperMock = ContactSyncMock.GetMapper();

            _handler = new SyncMockApiContactsToMailchimpRequestHandler(_mediatorMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_SyncMockApiContactsToMailchimp()
        {
            // Act
            ContactSyncResult contactSyncResult = await _handler.Handle(new SyncMockApiContactsToMailchimpRequest(), CancellationToken.None);

            // Assert
            contactSyncResult.ShouldNotBeNull();
            contactSyncResult.SyncedContacts.ShouldBe(3);

            _mediatorMock.Verify(x => x.Send(It.IsAny<GetMockApiContactsRequest>(), It.IsAny<CancellationToken>()), Times.Once);
            _mediatorMock.Verify(x => x.Send(It.IsAny<UpdateMailchimpMembersCommand>(), It.IsAny<CancellationToken>()), Times.Once);
            _mapperMock.Verify(x => x.Map<List<MailchimpMember>>(It.IsAny<List<Contact>>()), Times.Once);
        }
    }
}