using System.Configuration;
using System.Threading.Tasks;

namespace SMSSender.REST.Helpers
{
    public enum Contact
    {
        A, B, C
    }

    public static class ConfigurationHelper
    {
        public static Task<string> GetClockworkApiKeyAsync() { return Task.Factory.StartNew(GetClockworkApiKey); }
        public static string GetClockworkApiKey()
        {
            string value = ConfigurationManager.AppSettings["Clockwork_APIKey"];
            return !string.IsNullOrEmpty(value) ? value : null;
        }

        public static Task<string> GetTextMessageFromCodeAsync() { return Task.Factory.StartNew(GetTextMessageFromCode); }
        public static string GetTextMessageFromCode()
        {
            string value = ConfigurationManager.AppSettings["TextMessageFrom"];
            return !string.IsNullOrEmpty(value) ? value : null;
        }

        public static Task<int?> GetTextMessageMaxLengthAsync() { return Task.Factory.StartNew(GetTextMessageMaxLength); }
        public static int? GetTextMessageMaxLength()
        {
            int length;
            if (int.TryParse(ConfigurationManager.AppSettings["TextMessageMaxLength"], out length) && length > 0)
                return length;
            return null;
        }

        public static Task<string> GetContactANumberAsync() { return Task.Factory.StartNew(GetContactANumber); }
        public static string GetContactANumber()
        {
            string value = ConfigurationManager.AppSettings["Contact_A"];
            return !string.IsNullOrEmpty(value) ? value : null;
        }
        public static Task<string> GetContactBNumberAsync() { return Task.Factory.StartNew(GetContactBNumber); }
        public static string GetContactBNumber()
        {
            string value = ConfigurationManager.AppSettings["Contact_B"];
            return !string.IsNullOrEmpty(value) ? value : null;
        }
        public static Task<string> GetContactCNumberAsync() { return Task.Factory.StartNew(GetContactCNumber); }
        public static string GetContactCNumber()
        {
            string value = ConfigurationManager.AppSettings["Contact_C"];
            return !string.IsNullOrEmpty(value) ? value : null;
        }
    }
}
