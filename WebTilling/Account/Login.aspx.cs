using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebTilling.Models;

namespace WebTilling.Account {
    public partial class Login : Page {
        protected void Page_Load(object sender, EventArgs e) {
            //RegisterHyperLink.NavigateUrl = "Register";
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl)) {
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
            ////if (!IsPostBack) {//double post??? User object won't be populated until the next request. 
            //////http://stackoverflow.com/questions/14508495/user-identity-isauthenticated-returns-false-after-setting-cookie-and-validating?rq=1
            //////I've seen some cases where the context (which is based on ASP.NET's session) gets out of sync with Identity's tokens.
            ////    if (Context.User.Identity.IsAuthenticated) {
            ////        IdentityHelper.RedirectToReturnUrl("~/Search", Response);
            ////    }
            ////}
        }
        protected void LogIn(object sender, EventArgs e) {
            if (IsValid) {
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                ApplicationUser user = manager.FindByName(Email.Text.ToLower());
                if (user != null) {//Add this to check if the email was confirmed.
                    if (!manager.IsEmailConfirmed(user.Id)) {
                        FailureText.Text = "You need a confirmation from Project Leader.";
                        ErrorMessage.Visible = true;
                    } else {
                        var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
                        // This doesn't count login failures towards account lockout. To enable password failures to trigger lockout, change to shouldLockout: true
                        if (signinManager.PasswordSignIn(Email.Text.ToLower(), Password.Text, false, shouldLockout: false) == SignInStatus.Success) {// Validate the user password
                            //IdentityHelper.RedirectToReturnUrl("~/Account/Login", Response); //double post??? 
                            IdentityHelper.RedirectToReturnUrl("~/Search", Response);
                        } else {
                            FailureText.Text = "Invalid login attempt";
                            ErrorMessage.Visible = true;
                        }
                    }
                } else {
                    IdentityHelper.RedirectToReturnUrl("~/Account/Register", Response);
                }
                //var result = signinManager.PasswordSignIn(Email.Text, Password.Text, false, shouldLockout: false);
                ////var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);
                //switch (result) {
                //    case SignInStatus.Success:
                //        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //        IdentityHelper.RedirectToReturnUrl("~/Search", Response);
                //        break;
                //    case SignInStatus.LockedOut:
                //        Response.Redirect("~/Account/Lockout");
                //        break;
                //    //case SignInStatus.RequiresVerification:
                //    //    Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                //    //                                    Request.QueryString["ReturnUrl"], RememberMe.Checked), true);
                //    //    break;
                //    case SignInStatus.Failure:
                //    default:
                //        FailureText.Text = "Invalid login attempt";
                //        ErrorMessage.Visible = true;
                //        break;
                //}
            }
        }
    }
}