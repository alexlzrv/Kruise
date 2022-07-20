using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kruise.API;

public class AuthOptions
{
    public const string Secret = "test";

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }
}
