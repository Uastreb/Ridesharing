using RideSharingApp.BLL.Interfaces;
using RideSharingApp.BLL.Services;
using RideSharingApp.WEB.Helpers;
using RideSharingAPP.BLL.DTO.AccountInformationDTO;
using RideSharingAPP.BLL.Interfaces.IValidations;
using RideSharingAPP.WEB.Models.Authorization;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace RideSharingApp.WEB.Controllers
{
    /// <summary>The controller in which logging in, registering, restoring access to the account.</summary>
    public class AuthorizationController : Controller
    {

        /// <summary>Variable storing role under which the user is authorized</summary>
        public static string Role;

        IAccountInformationValidation accountInformationValidation;
        IAccountInformationService accountService;


        /// <summary>Controller constructor.</summary>
        /// <value>Accepts the interface of the validation service as well as the service table of accounts.</value>
        /// <remarks>Constructor - a function that fires before other constructor methods.</remarks>
        public AuthorizationController(IAccountInformationService serv, IAccountInformationValidation val)
        {
            accountInformationValidation = val;
            accountService = serv;
        }


        /// <summary>Returns an atorization form</summary>
        /// <remarks>Form that allows the user to enter the application or go to the registration form, password recovery.</remarks>
        /// <returns>View()</returns>
        [HttpGet]
        public ActionResult Authorization()
        {
            return View();
        }


        /// <summary>Post version of authorization request.</summary>
        /// <remarks>The post version of the authorization request returns the data transmitted by the user to the server.</remarks>
        /// <returns>The menu form for successful authorization and the same form for not successful.</returns>
        /// <value>A model created to store the data required for entry, as well as the selected role.</value>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Authorization(InputViewModel account, string role)
        {
            var mappedAccount = new AutoMap<AccountInformationDTOCreditionals, InputViewModel>().Initialize(account);
            var error = accountInformationValidation.IsValidAuthorization(mappedAccount);
            if (error == null)
            {
                int? accountId = await accountService.Authorization(mappedAccount);
                if (accountId != null)
                {
                    Role = role;
                    FormsAuthentication.SetAuthCookie(account.Email, true);
                    HttpContext.Response.Cookies["AccountInformationId"].Value = accountId.ToString();
                    switch (Role)
                    {
                        case "Passenger":
                            return RedirectToAction("RegistrationRoutesList", "Client");
                        case "Driver":
                            return RedirectToAction("RouteList", "Driver");
                    }
                }
                else
                {
                    ModelState.AddModelError("", RideSharingAPP.WEB.Properties.Settings.Default.InvalidDateAuthoriz);
                }
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View();
        }

        
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Response.Cookies["AccountInformationId"].Value = "";
            return RedirectToAction("Authorization");
        }

        /// <summary>Returns an registration form</summary>
        /// <remarks>Form allowing you to register in the application.</remarks>
        /// <returns>View()</returns>
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }



        /// <summary>Post version of registration request.</summary>
        /// <remarks>The post version of the registration request returns the data by the user to the server.</remarks>
        /// <returns>The menu form for successful authorization and the same form for not successful.</returns>
        /// <value>A model created to store the data required for entry, as well as the selected role.</value>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel account, string role)
        {
            var mappedAccount = new AutoMap<AccountInformationDTOCreditionals, RegistrationViewModel>().Initialize(account);
            var error = await accountInformationValidation.IsValidRegistration(mappedAccount);
            if (error == null)
            {
                int? newAccountsId = await accountService.Registration(mappedAccount);
                
                HttpContext.Response.Cookies["AccountInformationId"].Value = newAccountsId.ToString();

                await SendEmail.SendAsync(account.Email, "Поздравляем с регистрацией", "Служба поддержки");
                return RedirectToAction("Authorization");
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View();
        }


        /// <summary>Returns password recovery form.</summary>
        /// <remarks>A form that allows you to restore access to your account.</remarks>
        /// <returns>View()</returns>
        [HttpGet]
        public ActionResult PasswordRecovery()
        {
            return View();
        }


        /// <summary>Post version of the request to restore account access.</summary>
        /// <remarks>It receives from the client the data required to restore access to the account.</remarks>
        /// <returns>Sends a new password to the mail and also redirects to the login form.</returns>
        /// <value>Model containing user mail.</value>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> PasswordRecovery(PasswordRecoveryView account)
        {
            var mappedAccount = new AutoMap<AccountInformationDTOGetEmail, PasswordRecoveryView>().Initialize(account);
            var error = await accountInformationValidation.IsValidPasswordRecovery(mappedAccount);
            if (error == null)
            {
                string password = await accountService.PasswordRecovery(account.Email);
                bool check = await SendEmail.SendAsync(account.Email, "Восстановление пароля", $"Сообщаем Вам, что Ваш пароль на сервере RideSharing был изменен, новый пароль: {password} \nВы можете изменить его в личном кабинете на сайте.");
                if (check)
                {
                    return RedirectToAction("Authorization", "Authorization");
                }
                else
                {
                    ModelState.AddModelError("", RideSharingAPP.WEB.Properties.Settings.Default.SendMessageEror);
                }
            }
            else
            {
                ModelState.AddModelError("", error.Message);
            }
            return View();
        }


        /// <summary>Invokes a method to free database connection memory.</summary>
        protected override void Dispose(bool disposing)
        {
            accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}