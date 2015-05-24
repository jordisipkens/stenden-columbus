using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebserviceColumbus.Authorization
{
    public class TokenRequired : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            string token = getTokenHeader(actionContext);
            return TokenManager.IsAuthorized(token);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        private string getTokenHeader(HttpActionContext actionContext)
        {
            IEnumerable<string> values;
            if (actionContext.Request.Headers.TryGetValues("Token", out values) && values.Count() > 0) {
                return values.First().Replace("\"", "");
            }
            return null;
        }
    }
}