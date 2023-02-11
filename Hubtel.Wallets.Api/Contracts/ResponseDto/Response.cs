using System.Collections.Generic;
namespace Agents.Api.Contracts.ResponseDtos
{
    /// <summary>
    /// class defines custom error object
    /// </summary>
    public class ErrorModel
    {
        public string FieldName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// class defines error response
    /// </summary>
    public class ErrorResponse
    {
        public List<ErrorModel> Errors = new List<ErrorModel>() { };
    }

    /// <summary>
    /// class defines a response object with errors
    /// </summary>
    public class ResponseWithErrors<T>
    {
        public int statusCode { get; set; }
        public T errors { get; set; }
    }

    /// <summary>
    /// class defines a response object with only error message
    /// </summary>
    public class ResponseWithError
    {
        public int statusCode { get; set; }
        public string message { get; set; } = string.Empty;
    }

    /// <summary>
    /// class defines a response object data
    /// </summary>
    public class SuccessWithDataResponse<T>
    {
        public int statusCode { get; set; }
        public T data { get; set; }

        public string message { get; set; } = string.Empty;
    }

    /// <summary>
    /// class defines custom s3 response
    /// </summary>
    public class S3Response
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
