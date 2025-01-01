namespace Contact.API.Contacts.CreateContact
{
    public class DeleteContactRequestValidator : Validator<DeleteContactRequest>
    {
        public DeleteContactRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
        }
    }
}
