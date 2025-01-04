using BuildingBlocks.Dtos.ContactDtos;

namespace Report.API.Services.Contact
{
    public interface IContactService
    {
        Task<IList<ContactDto>> GetReportsAsync();
    }
}
