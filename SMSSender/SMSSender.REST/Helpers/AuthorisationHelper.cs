using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSSender.REST.Helpers
{
    public static class AuthorisationHelper
    {
        public static bool IsRequestValid(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            string tokenArray = ConfigurationHelper.GetAccessTokens();

            if(string.IsNullOrEmpty(tokenArray))
                return false;

            string[] accesTokens = tokenArray.Split(',');
            return accesTokens.Any(accesToken => accesToken == token); // See if the 'token' is present in the array of valid tokens.
        }
    }
}
