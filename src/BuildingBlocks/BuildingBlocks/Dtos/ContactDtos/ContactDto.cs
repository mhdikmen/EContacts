namespace BuildingBlocks.Dtos.ContactDtos
{
    public record ContactDto
    {
        public ContactDto()
        {
            ContactDetails = [];
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public virtual IList<ContactDetailDto> ContactDetails { get; set; }
    }
}
