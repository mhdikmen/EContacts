using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text;

namespace Contact.API.Middlewares
{
    public class SerilogRequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public SerilogRequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Guid transactionId = Guid.NewGuid();
            var request = context.Request;

            // Enable buffering to read the request body
            request.EnableBuffering();

            string requestBody = string.Empty;
            if (request.HasFormContentType)
            {
                requestBody = await ReadFormDataAsync(request);
            }
            else
            {
                requestBody = await ReadRequestBodyAsync(request);
            }
            Log.Information("Tid: {TransactionId} Request Body: {Body}", transactionId, requestBody);

            // Reset the request body stream position
            request.Body.Position = 0;

            // Capture the original response body stream
            var originalBodyStream = context.Response.Body;

            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                try
                {
                    // Proceed with the pipeline
                    await _next(context);

                    // Log the response body
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                    Log.Information("Tid: {TransactionId} Response: {StatusCode} {ResponseBody}", transactionId, context.Response.StatusCode, responseBody);

                    // Copy the response back to the original stream
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(originalBodyStream);
                }
                finally
                {
                    // Ensure streams are reset and properly disposed
                    context.Response.Body = originalBodyStream;
                }
            }
        }

        private static async Task<string> ReadFormDataAsync(HttpRequest request)
        {
            var formData = await request.ReadFormAsync();
            var formDataStringBuilder = new StringBuilder();

            foreach (var key in formData.Keys)
            {
                formDataStringBuilder.AppendLine($"Key: {key}, Value: {formData[key]}");
            }

            foreach (var file in formData.Files)
            {
                formDataStringBuilder.AppendLine($"Key: {file.Name}, FileName: {file.FileName}, ContentType: {file.ContentType}, Length: {file.Length}");
            }

            return formDataStringBuilder.ToString();
        }

        private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            // Read the request body stream without disposing it
            request.Body.Position = 0;
            using (var memoryStream = new MemoryStream())
            {
                await request.Body.CopyToAsync(memoryStream);
                request.Body.Position = 0;
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
    }
}
