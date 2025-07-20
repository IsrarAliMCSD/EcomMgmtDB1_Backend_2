using Code_EcomMgmtDB1.Models;

namespace Code_EcomMgmtDB1.JwtHelper
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public User UserData { get; set; }
    }
}
