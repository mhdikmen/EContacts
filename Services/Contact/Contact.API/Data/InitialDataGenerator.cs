using Bogus;
using BuildingBlocks.Enums;
using Contact.API.Models;

namespace Contact.API.Data
{
    public class InitialDataGenerator
    {
        private readonly string _internationalPhoneNumberFormatForFaker = "+## ### #### ###";
        private readonly List<Models.Contact> _contacts;
        private readonly List<Models.ContactDetail> _contactDetails;
        public InitialDataGenerator()
        {
            _contacts = GenerateContacts();
            _contactDetails = GenerateContactDetails();
        }
        private List<Models.Contact> GenerateContacts()
        {
            var contactFaker = new Faker<Models.Contact>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Surname, f => f.Name.LastName())
                .RuleFor(u => u.CompanyName, f => f.Company.CompanyName());
            return contactFaker.Generate(1000);
        }

        private List<ContactDetail> GenerateContactDetails()
        {
            List<ContactDetail> contactDetails = new();

            _contacts.ForEach(contact =>
            {
                foreach (ContactDetailType type in Enum.GetValues(typeof(ContactDetailType)))
                {
                    var detail = new ContactDetail
                    {
                        Id = Guid.NewGuid(),
                        ContactId = contact.Id, 
                        Type = type,
                        Content = type switch
                        {
                            ContactDetailType.PhoneNumber => new Faker().Phone.PhoneNumber(_internationalPhoneNumberFormatForFaker),
                            ContactDetailType.EmailAddress => new Faker().Internet.Email(),
                            ContactDetailType.Location => new Faker().Address.Country(),
                            _ => throw new NotSupportedException($"ContactDetailType {type} is not supported.")
                        }
                    };

                    contactDetails.Add(detail);
                }
            });

            return contactDetails;
        }

        public List<Models.Contact> GetContacts() => _contacts;
        public List<Models.ContactDetail> GetContactDetails() => _contactDetails;

    }
}
