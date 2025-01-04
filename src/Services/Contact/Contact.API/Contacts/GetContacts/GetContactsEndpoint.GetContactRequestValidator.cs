namespace Contact.API.Contacts.GetContacts
{
    public class GetContactsRequestValidator : Validator<GetContactsRequest>
    {
        public GetContactsRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"{nameof(GetContactsRequest.PageIndex)} must be greater than or equal to 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage($"{nameof(GetContactsRequest.PageSize)} must be greater than 0.");
        }
    }
}
