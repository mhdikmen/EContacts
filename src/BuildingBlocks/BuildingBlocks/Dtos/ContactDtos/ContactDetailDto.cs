using BuildingBlocks.Enums;

namespace BuildingBlocks.Dtos.ContactDtos
{
    public record ContactDetailDto
    {
        public Guid Id { get; set; }
        public ContactDetailType Type { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
    }
}
