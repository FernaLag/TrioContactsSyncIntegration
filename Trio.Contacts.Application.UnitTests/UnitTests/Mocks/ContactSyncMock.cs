using AutoMapper;
using MediatR;
using Moq;
using Trio.ContactSync.Application.Features.ContactSync.Queries;
using Trio.ContactSync.Application.Features.Mailchimp.Commands;
using Trio.ContactSync.Application.Features.MockApi.Queries;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Application.UnitTests.UnitTests.Mocks
{
    public static class ContactSyncMock
    {
        public static Mock<IMediator> GetMediator()
        {
            List<Contact> contacts =
            [
                new() { FirstName = "Alex", LastName = "Test", Email = "alex@gmail.com" },
                new() { FirstName = "Maria", LastName = "Test", Email = "maria@outlook.com" },
                new() { FirstName = "Natalia", LastName = "Test", Email = "natalia@hotmail.com" }
            ];

            ContactSyncResult contactSyncResult = new(contacts.Count, contacts);

            Mock<IMediator> mediatorMock = new();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetMockApiContactsRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(contacts);

            mediatorMock.Setup(x => x.Send(It.IsAny<SyncMockApiContactsToMailchimpRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(contactSyncResult);

            mediatorMock.Setup(x => x.Send(It.IsAny<UpdateMailchimpMembersCommand>(), It.IsAny<CancellationToken>()));

            return mediatorMock;
        }

        public static Mock<IMapper> GetMapper()
        {
            Mock<IMapper> mapperMock = new();

            mapperMock.Setup(x => x.Map<List<MailchimpMember>>(It.IsAny<List<Contact>>()))
                .Returns(It.IsAny<List<MailchimpMember>>());

            return mapperMock;
        }
    }
}