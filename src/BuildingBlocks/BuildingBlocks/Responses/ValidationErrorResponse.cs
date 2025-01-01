using BuildingBlocks.Consts;
namespace BuildingBlocks.Responses
{
    public record ValidationErrorResponse : BaseResponse
    {
        public ValidationErrorResponse(List<string> errors) : base(System.Net.HttpStatusCode.BadRequest, Messages.ValidationErrorMessage)
        {
            Errors = errors;
        }
        public List<string> Errors { get; set; }
    }
}
