using Code_EcomMgmtDB1.Models;

namespace Code_EcomMgmtDB1.JwtHelper
{
    public interface IJwtTokenProvider
    {
        Tokens GenerateToken(User user);
    }
}
