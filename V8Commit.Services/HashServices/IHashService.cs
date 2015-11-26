
using System.Security;

namespace V8Commit.Services
{
    public interface IHashService
    {
        string HashString(string source);
        string HashSecure(SecureString source);
    }
}
