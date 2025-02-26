using System.Collections.Generic;

namespace OdiApp.DTOs.SharedDTOs
{
    public class OdiResponse<T>
    {

        public int StatusCode { get; set; } //private set kaldırıldı
        public bool IsSuccessful { get; set; } //private set kaldırıldı

        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public T Data { get; set; } //private set kaldırıldı

        //static factory methods
        public static OdiResponse<T> Success(string message, T data, int statusCode)
        {
            return new OdiResponse<T> { Data = data, StatusCode = statusCode, IsSuccessful = true, Message = message };
        }

        public static OdiResponse<T> Success(string message, int statusCode)
        {
            return new OdiResponse<T> { Data = default, StatusCode = statusCode, IsSuccessful = true, Message = message };
        }

        public static OdiResponse<T> Fail(string message, List<string> errors, int statusCode)
        {
            return new OdiResponse<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false, Message = message };
        }

        public static OdiResponse<T> Fail(string message, string error, int statusCode)
        {
            return new OdiResponse<T> { Errors = new List<string> { error }, StatusCode = statusCode, IsSuccessful = false, Message = message };
        }
    }
}