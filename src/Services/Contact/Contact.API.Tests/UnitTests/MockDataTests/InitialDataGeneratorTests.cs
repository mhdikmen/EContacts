using System;
using System.Linq;
using BuildingBlocks.Enums;
using Contact.API.Data;
using Contact.API.Models;
using FluentAssertions;
using Xunit;

namespace Contact.API.Tests.UnitTests.MockDataTests
{
    public class InitialDataGeneratorTests
    {
        [Fact]
        public void Should_Generate_Contacts_With_Valid_Fields()
        {
            int numberOfContacts = 5;
            var dataGenerator = new InitialDataGenerator(numberOfContacts);
            var contacts = dataGenerator.GetContactWithDetails();

            contacts.Should().NotBeNullOrEmpty();
            contacts.Should().HaveCount(numberOfContacts);
            contacts[0].Id.Should().NotBeEmpty();
            contacts[0].Name.Should().NotBeNullOrWhiteSpace();
            contacts[0].Surname.Should().NotBeNullOrWhiteSpace();
            contacts[0].CompanyName.Should().NotBeNullOrWhiteSpace();
            contacts[0].CreatedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
