namespace BearerAuthExample.Requests
{
    public class GrantRequest
    {
        public string Grant_Type { get; set; }

        public string Refresh_Token { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
