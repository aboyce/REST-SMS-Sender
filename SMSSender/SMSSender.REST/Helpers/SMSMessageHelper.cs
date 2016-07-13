using System;
using System.Net;
using Clockwork;

namespace SMSSender.REST.Helpers
{
    public static class SMSMessageHelper
    {
        /// <summary>
        /// Will try to convert the string from the request into a valid contact.
        /// </summary>
        /// <returns>The type of Contact extracted.</returns>
        public static Contact GetContactFromString(string contactLetter)
        {
            switch (contactLetter.ToUpper())
            {
                case "A":
                    return Contact.A;
                case "B":
                    return Contact.B;
                case "C":
                    return Contact.C;
                default:
                    return Contact.Error;
            }
        }

        /// <summary>
        /// Will try to retrieve the correct mobile number from the configuration depending on the contact type requested.
        /// </summary>
        /// <returns>The number if successful, null if not</returns>
        private static string GetPhoneNumberForContact(Contact contact)
        {
            string number;
            switch (contact)
            {
                case Contact.A:
                    number = ConfigurationHelper.GetContactANumber();
                    return !string.IsNullOrEmpty(number) ? number : null;
                case Contact.B:
                    number = ConfigurationHelper.GetContactBNumber();
                    return !string.IsNullOrEmpty(number) ? number : null;
                case Contact.C:
                    number = ConfigurationHelper.GetContactCNumber();
                    return !string.IsNullOrEmpty(number) ? number : null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Will try to send a SMS Message to the contact requested.
        /// </summary>
        /// <returns>An error message (string) or null if successful.</returns>
        public static string SendTextMessage(Contact contact, string message)
        {
            try
            {
                string apiKey = ConfigurationHelper.GetClockworkApiKey();
                if (string.IsNullOrEmpty(apiKey))
                    return "Error: Internal : API Key could not be obtained";
                string to = GetPhoneNumberForContact(contact);
                if (string.IsNullOrEmpty(to))
                    return "Error: Internal : Phone number for contact could not be obtained.";
                string from = ConfigurationHelper.GetTextMessageFromCode();
                if (string.IsNullOrEmpty(from))
                    return "Error: Internal : From code could not be obtained.";

                SMSResult result = new API(apiKey).Send(
                    new SMS
                    {
                        To = to,
                        From = from,
                        Message = message 
                    });

                if (result.Success)
                    return null;
                else
                {
                    return $"Error: Internal : SMS Error : {result.ErrorMessage}";
                }
            }
            catch (APIException ex)
            {
                // You’ll get an API exception for errors
                // such as wrong username or password
                return $"Error: Internal : SMS Error : {ex.Message}";
            }
            catch (WebException ex)
            {
                // Web exceptions mean you couldn’t reach the Clockwork server
                return $"Error: Internal : SMS Error : {ex.Message}";
            }
            catch (ArgumentException ex)
            {
                // Argument exceptions are thrown for missing parameters,
                // such as forgetting to set the username
                return $"Error: Internal : SMS Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                // Something else went wrong, the error message should help
                return $"Error: Internal : SMS Error : {ex.Message}";
            }
        }
    }
}
