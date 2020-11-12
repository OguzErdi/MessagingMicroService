namespace User.Application.Models
{
    public class UserTokenModel
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public UserTokenModel(string username, string token)
        {
            Username = username;
            Token = token;
        }
    }
}