using System.ComponentModel.DataAnnotations;

namespace Base.Utils
{
    public static class StringExtensions
    {
        public static string OrIfNullOrEmtry(this string str, string other)
        {
            return string.IsNullOrWhiteSpace(str) ? other : str;
        }
        public static bool IsValidEmailString(this string str)
        {
            EmailAddressAttribute helper = new EmailAddressAttribute();
            return helper.IsValid(str);
        }
    }
}
