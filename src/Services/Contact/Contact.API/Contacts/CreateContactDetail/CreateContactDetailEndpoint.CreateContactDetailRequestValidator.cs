namespace Contact.API.Contacts.CreateContactDetail
{
    public class CreateContactDetailRequestValidator : Validator<CreateContactDetailRequest>
    {
        public CreateContactDetailRequestValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty()
                .WithMessage($"{nameof(CreateContactDetailRequest.ContactId)} is required.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage($"Enter a valid {nameof(CreateContactDetailRequest.Type)}.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage($"{nameof(CreateContactDetailRequest.Content)} is required.");
        }
    }
}
