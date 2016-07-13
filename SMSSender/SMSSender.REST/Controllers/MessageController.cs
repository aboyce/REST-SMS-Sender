using System.Web.Http;
using SMSSender.REST.Helpers;

namespace SMSSender.REST.Controllers
{
    public class MessageController : ApiController
    {
        [AcceptVerbs("GET")]
        public string Send(string accessToken, string contactLetter, string message)
        {
            // Check that everything is present before we begin
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(contactLetter) || string.IsNullOrEmpty(message))
                return "Error: Parameters are missing.";
            
            // See if the access token provided is one that we accept.
            if (!AuthorisationHelper.IsRequestValid(accessToken))
                return "Error: Authorisation has failed.";

            // See if the contact information it valid
            Contact contact = SMSMessageHelper.GetContactFromString(contactLetter);

            if (contact == Contact.Error)
                return "Error: Contact parameter could not be parsed.";

            // Try to send the SMS Message.
            return SMSMessageHelper.SendTextMessage(contact, message);
        }
    }
}
