using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API_DB_First.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        //Following Method will be called to check if the username and password is correct.
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            //If the username and password is or null
            if (actionContext.Request.Headers.Authorization == null)
            {
                //Here we have to send a response. The Response will be unauthorized.
                //In the Postman if the authorization key is unchecked then the status is 401 Unauthorized
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            //if the username and password is not null
            else
            {
                string encoded = actionContext.Request.Headers.Authorization.Parameter;
                string decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));

                string[] splittedText = decoded.Split(new char[] {':'});

                string username = splittedText[0];
                string password = splittedText[1];

                if (username == "rimon" && password == "rim123")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                }
                else
                {
                    //If the username and password did not match then we will send the Unauthorized status 401
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}