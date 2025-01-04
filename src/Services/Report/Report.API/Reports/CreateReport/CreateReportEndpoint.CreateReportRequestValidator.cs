namespace Report.API.Reports.CreateReport
{
    public class CreateReportRequestValidator : Validator<CreateReportRequest>
    {
        public CreateReportRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage($"{nameof(CreateReportRequest.Id)} is required.");
        }
    }
}
