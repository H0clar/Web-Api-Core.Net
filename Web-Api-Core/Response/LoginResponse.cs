namespace Web_Api_Core.Responses
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Token { get; set; }
    }
}
