using System.Web.Http;

namespace SMSSender.REST.Controllers
{
    public class MessageController : ApiController
    {
        [AcceptVerbs("GET")]
        public string Welcome()
        {
            return $"SMS Sender v{typeof(TestController).Assembly.GetName().Version}";
        }
    }
}
