using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth;

public class AuthOptions
{
    public const string ISSUER = "AirAstana";
    public const string AUDIENCE = "Someone";
    private const string KEY = "very_superPuper_megaOmega_secretKey!!!qwerty123";
    public const int LIFETIME = 1440;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}