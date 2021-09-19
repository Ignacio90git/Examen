using ApiComm;
using Model;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using Utils;

namespace Examen.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(UsuarioDTO user, string returnUrl)
        {
            var mResponse = new GenericResponse();
            if (ModelState.IsValid)
            {
                var apiResponse = await RestUtility.CallServiceAsync<GenericResponse>($"http://localhost:51571/api/validation", string.Empty, user, "POST", null) as GenericResponse;
                if (apiResponse == null)
                {
                    throw new Exception("Error");
                }
                if (apiResponse.Success)
                {
                    Session["jwt"] = "OK";
                    mResponse.Success = true;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    apiResponse.ViewRedirection = RedirectToLocal(returnUrl);
                }
                else
                {
                    mResponse.Success = false;
                }

            }

            return mResponse.ToJsonResult();
        }

        private string RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? returnUrl : returnUrl;
        }


        public ActionResult Registration()
        {
            return View();
        }


        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            // ClearSession();
            return Json("/Login/Login", JsonRequestBehavior.AllowGet);
        }

    }

}