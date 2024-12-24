using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trio.ContactSync.Application.Features.ContactSync.Queries;
using Trio.ContactSync.Domain;

namespace Trio.ContactSync.Api.Controllers
{
    [ApiController]
    [Route("contacts/sync")]
    public class ContactSyncController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> SyncContacts()
        {
            ContactSyncResult contactSyncResult = await _mediator.Send(new SyncMockApiContactsToMailchimpRequest());
            return Ok(contactSyncResult);
        }
    }
}