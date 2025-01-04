namespace Report.API.Reports.GetReports
{
    public class GetReportsRequestValidator : Validator<GetReportsRequest>
    {
        public GetReportsRequestValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(0)
                .WithMessage($"{nameof(GetReportsRequest.PageIndex)} must be greater than or equal to 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage($"{nameof(GetReportsRequest.PageSize)} must be greater than 0.");
        }
    }
}
