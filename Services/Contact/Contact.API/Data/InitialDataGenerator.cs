using Bogus;
using BuildingBlocks.Enums;
using Contact.API.Models;

namespace Contact.API.Data
{
    public class InitialDataGenerator
    {
        private readonly string _internationalPhoneNumberFormatForFaker = "+## ### #### ###";
        public List<Models.Contact> GenerateContacts()
        {
            var contactDetailFaker = new Faker<ContactDetail>()
                .RuleFor(d => d.Id, f => Guid.NewGuid())
                .RuleFor(d => d.Content, (f, d) =>
                {
                    return d.Type switch
                    {
                        ContactDetailType.PhoneNumber => f.Phone.PhoneNumber(_internationalPhoneNumberFormatForFaker),
                        ContactDetailType.EmailAddress => f.Internet.Email(),
                        ContactDetailType.Location => f.Address.Country(),
                        _ => throw new NotSupportedException($"ContactDetailType {d.Type} is not supported.")
                    };
                });

            var contactFaker = new Faker<Models.Contact>()
                .RuleFor(u => u.Id, f => f.Random.Guid())
                .RuleFor(u => u.Name, f => f.Name.FirstName())
                .RuleFor(u => u.Surname, f => f.Name.LastName())
                .RuleFor(u => u.CompanyName, f => f.Company.CompanyName())
                .RuleFor(u => u.ContactDetails, (f, c) =>
                {
                    var details = new List<ContactDetail>();

                    foreach (ContactDetailType type in Enum.GetValues(typeof(ContactDetailType)))
                    {
                        var detail = new ContactDetail
                        {
                            Id = Guid.NewGuid(),
                            ContactId = c.Id,
                            Contact = c,
                            Type = type,
                            Content = type switch
                            {
                                ContactDetailType.PhoneNumber => f.Phone.PhoneNumber(_internationalPhoneNumberFormatForFaker),
                                ContactDetailType.EmailAddress => f.Internet.Email(),
                                ContactDetailType.Location => f.Address.Country(),
                                _ => throw new NotSupportedException($"ContactDetailType {type} is not supported.")
                            }
                        };
                        details.Add(detail);
                    }

                    return details;
                });

            return contactFaker.Generate(1000);
        }
    }
}
