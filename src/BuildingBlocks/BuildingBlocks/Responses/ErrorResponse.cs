using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace BuildingBlocks.Responses
{
    public record ErrorResponse
    {
        public ErrorResponse()
        {

        }
        public ErrorResponse(string message)
        {
            Message = message;
        }
        public string Message { get; set; } = default!;
        public override string ToString() { return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }); }
    }
}
