namespace Contact.API.Contacts.CreateContact
{
    public class CreateContactRequestValidator : Validator<CreateContactRequest>
    {
        public CreateContactRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Surname is required.");

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("Company name is required.");
        }
    }
}
