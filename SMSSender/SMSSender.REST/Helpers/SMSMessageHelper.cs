using System;
using System.Net;
using Clockwork;

namespace SMSSender.REST.Helpers
{
    public class SMSMessageHelper
    {
        /// <summary>
        /// Will try to retrieve the correct mobile number from the configuration depending on the contact type requested.
        /// </summary>
        /// <returns>The number if successful, null if not</returns>
        private string GetPhoneNumberForContact(Contact contact)
        {
            string number;
            switch (contact)
            {
                case Contact.A:
                    number = ConfigurationHelper.GetContactANumber();
                    return string.IsNullOrEmpty(number) ?  number : null;
                case Contact.B:
                     number = ConfigurationHelper.GetContactBNumber();
                    return string.IsNullOrEmpty(number) ? number : null;
                case Contact.C:
                     number = ConfigurationHelper.GetContactBNumber();
                    return string.IsNullOrEmpty(number) ? number : null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Will try to send a SMS Message to the contact requested.
        /// </summary>
        /// <returns>A bool to indicate success.</returns>
        public bool SendTextMessage(Contact contact, string message)
        {
            try
            {
                string apiKey = ConfigurationHelper.GetClockworkApiKey();
                if (string.IsNullOrEmpty(apiKey))
                    return false;
                string to = GetPhoneNumberForContact(contact);
                if (string.IsNullOrEmpty(to))
                    return false;
                string from = ConfigurationHelper.GetTextMessageFromCode();
                if (string.IsNullOrEmpty(from))
                    return false;

                SMSResult result = new API(apiKey).Send(
                    new SMS
                    {
                        To = to,
                        From = from,
                        Message = message 
                    });

                if (result.Success)
                    return true;
                else
                {
                    return false;
                    //result.SMS.To
                    //result.ErrorCode
                    //result.ErrorMessage
                }
            }
            catch (APIException ex)
            {
                // You’ll get an API exception for errors
                // such as wrong username or password
                return false;
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn’t reach the Clockwork server
                return false;
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                return false;
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                return false;
            }
        }
    }
}
