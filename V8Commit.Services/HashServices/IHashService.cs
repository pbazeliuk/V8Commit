using System.Security;

namespace V8Commit.Services.HashServices
{
    public interface IHashService
    {
        string HashString(string source);
        string HashSecure(SecureString source);
    }
}
