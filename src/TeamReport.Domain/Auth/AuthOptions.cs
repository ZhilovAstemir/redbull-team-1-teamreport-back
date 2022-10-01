using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TeamReport.Domain.Auth;
public class AuthOptions
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    const string Key = "mysupersecret_secretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
