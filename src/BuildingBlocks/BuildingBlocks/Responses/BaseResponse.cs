using Newtonsoft.Json;
using System.Net;

namespace BuildingBlocks.Responses
{
    public record BaseResponse
    {
        public BaseResponse()
        {

        }
        public BaseResponse(HttpStatusCode httpStatusCode, string message)
        {
            HttpStatusCode = (int)httpStatusCode;
            Message = message;
        }
        public int HttpStatusCode { get; set; }
        public string Message { get; set; } = default!;

        public override string ToString() { return JsonConvert.SerializeObject(this); }
    }
}
