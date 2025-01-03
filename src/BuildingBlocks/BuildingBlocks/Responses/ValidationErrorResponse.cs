using BuildingBlocks.Consts;
namespace BuildingBlocks.Responses
{
    public record ValidationErrorResponse : ErrorResponse
    {
        public ValidationErrorResponse(List<string> errors) : base(Messages.ValidationErrorMessage)
        {
            Errors = errors;
        }
        public List<string> Errors { get; set; }
    }
}
