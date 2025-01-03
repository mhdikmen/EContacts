using BuildingBlocks.Dtos.ContactDtos;

namespace Contact.API.Contacts.GetContact
{
    public record GetContactQueryResult(bool IsExists, ContactDto? Contact = null);
}
