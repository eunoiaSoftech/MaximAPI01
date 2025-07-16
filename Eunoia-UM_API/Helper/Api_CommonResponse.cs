namespace Eunoia_UM_API.Helper
{
    public class Api_CommonResponse
    {
        public int statusCode { get; set; }
        public int responseCode { get; set; }
        public string? message { get; set; }
        public object? data { get; set; }
        public object? data1 { get; set; }
        public object? Rights { get; set; }
        public string? userCode { get; set; }
        public int status { get; set; }
        public int mobileOTP { get; set; }
        public int emailOTP { get; set; }
        public string? userID { get; set; }
    }
    public class VerificationRequest
    {
        public string? userID { get; set; }
        public string? verificationType { get; set; }

    }
}
