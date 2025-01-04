namespace Report.API.Reports.GetReport
{
    public class GetReportRequestValidator : Validator<GetReportRequest>
    {
        public GetReportRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage($"{nameof(GetReportRequest.Id)} is required.");
        }
    }
}
