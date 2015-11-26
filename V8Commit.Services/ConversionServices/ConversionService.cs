using System.Security;

namespace V8Commit.Services
{
    public static class ConversionService
    {
        public static SecureString ToSecureString(string source)
        {
            SecureString result = new SecureString();
            if (string.IsNullOrEmpty(source))
            {
                return result;
            }

            foreach (char c in source.ToCharArray())
            {
                result.AppendChar(c);
            }

            return result;
        }


    }
}
