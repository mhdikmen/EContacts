namespace Contact.API.Contacts.DeleteContactDetail
{
    public class DeleteContactDetailRequestValidator : Validator<DeleteContactDetailRequest>
    {
        public DeleteContactDetailRequestValidator()
        {
            RuleFor(x => x.ContactId)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteContactDetailRequest.ContactId)} is required.");

            RuleFor(x => x.ContactDetailId)
                .NotEmpty()
                .WithMessage($"{nameof(DeleteContactDetailRequest.ContactDetailId)} is required.");
        }
    }
}
