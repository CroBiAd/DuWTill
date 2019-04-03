using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using WebTilling.Models;
using System.Net.Mail;

namespace WebTilling.Account {
    public partial class Register : Page {
        protected void CreateUser_Click(object sender, EventArgs e) {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            var user = new ApplicationUser() { UserName = Email.Text.ToLower(), Email = Email.Text.ToLower(), EmailConfirmed = false };
            IdentityResult result = manager.Create(user, Password.Text);
            if (result.Succeeded) {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                //string code = manager.GenerateEmailConfirmationToken(user.Id);
                //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
                //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

                //signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                using (SmtpClient client = new SmtpClient("adelaide-edu-au.mail.protection.outlook.com", 25)) {
                    MailMessage mes = new MailMessage("penny.tricker@adelaide.edu.au", "penny.tricker@adelaide.edu.au", "New user", "User: " + user.UserName);
                    client.Send(mes);
                }
                //ErrorMessage.Text = "User " + user.UserName + " has been registered. Please, await a confirmation from a Project Leader.";
                labError.Text = "User " + user.UserName + " has been registered. Please, await a confirmation from a Project Leader.";
                labError.CssClass = "text-success";
            } else {
                //ErrorMessage.Text = result.Errors.FirstOrDefault();
                labError.Text = result.Errors.FirstOrDefault();
                labError.CssClass = "text-danger";
            }
        }
    }
}