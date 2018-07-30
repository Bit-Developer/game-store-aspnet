using GameStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace GameStore.WebUI.Apis
{
    public class BaseApiController : ApiController
    {
        protected AppUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        protected AppRoleManager RoleManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        protected string GetErrorMessage(IdentityResult result)
        {
            string message = "";
            foreach (string error in result.Errors)
            {
                message = String.Concat(error, Environment.NewLine);
            }

            return message;
        }
    }
}
