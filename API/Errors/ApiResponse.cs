using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            // Null coalesing operator "??". this means if message is null then execute the thiing following.
            Message = message ?? GetDefaultMeaageForStatusCode(statusCode);
        }



        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMeaageForStatusCode(int statusCode)
        {
            // this is a new type of switch statement
            return statusCode switch
            {
                400 => "A bad request you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => @"Errors are the path to the dark side. Errors lead to anger, 
                Anger leads to hate. Hate leads to career change.",
                _ => null
            };
        }
    }
}