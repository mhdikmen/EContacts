namespace Contact.API.Contacts.GetContact
{
    public class GetContactRequestValidator : Validator<GetContactRequest>
    {
        public GetContactRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage($"{nameof(GetContactRequest.Id)} is required.");
        }
    }
}
