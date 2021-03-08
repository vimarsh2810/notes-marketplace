using InitialNotesMarketplace.Models;
using InitialNotesMarketplace.Models.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace InitialNotesMarketplace.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(login model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (var context = new DBEntities())
            {
                User loggedInUser = context.Users.First(x => x.EmailID == model.EmailID);
                bool isValid = context.Users.Any(x => x.EmailID == model.EmailID && x.Password == model.Password);
                //System.Diagnostics.Debug.WriteLine(loggedInUser.RoleID);
                if (isValid && (loggedInUser.RoleID == 1 || loggedInUser.RoleID == 2) && loggedInUser.IsEmailVerified == true)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    Session["user"] = loggedInUser;
                    return RedirectToAction("AdminDashboard", "Admin");
                }

                if (isValid && loggedInUser.RoleID == 3 && loggedInUser.IsEmailVerified == true)
                {
                    FormsAuthentication.SetAuthCookie(model.EmailID, false);
                    //System.Diagnostics.Debug.WriteLine(loggedInUser.EmailID);
                    Session["user"] = loggedInUser;
                    return RedirectToAction("UserDashboard", "RegisteredUser");
                }
                ViewBag.CustomMessage = "Incorrect Password";
                return View();
            }
        }
        
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(FormCollection collection)
        {
            string email = collection["emailId"];
            
            DBEntities _db = new DBEntities();
            if(_db.Users.Any(x => x.EmailID == email))
            {
                User user = _db.Users.FirstOrDefault(x => x.EmailID == email);
                var verifyUrl = "/Account/Login";
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

                //Enter your email id and display name
                var fromEmail = new MailAddress("", "");
                var toMail = new MailAddress(user.EmailID);

                //Enter your password
                var fromEmailPassword = "";

                string password = Membership.GeneratePassword(8, 1);
                user.Password = password;
                _db.SaveChanges();

                string emailSubject = "New Password";
                string emailBody = "Hello " + user.Firstname + "<br><br>Use the below link and password to log into your account<br><br>New Password: " + password + "<br><br>Login: <a href='" + link + "'>" + link + "</a><br><br>" + "Regards, <br>" + "Notes Marketplace";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var message = new MailMessage(fromEmail, toMail)
                {
                    Subject = emailSubject,
                    Body = emailBody,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return RedirectToAction("ForgotPassword", "Account");
            }
            
            
            
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(signup model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User newUser = new User()
            {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                EmailID = model.EmailID,
                Password = model.Password,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            using (var context = new DBEntities())
            {
                newUser.RoleID = 3;
                context.Users.Add(newUser);
                try
                {
                    context.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
            }
            if(model.Password == model.ConfirmPassword)
            {
                SendEmailVerificationMail(newUser);
                return RedirectToAction("Login", "Account");
            }
            return RedirectToAction("Signup", "Account");
            
        }

        public ActionResult EmailVerification(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailVerification(User model)
        {
            using (DBEntities db = new DBEntities())
            {
                User user = db.Users.FirstOrDefault(x => x.ID == model.ID);
                user.IsEmailVerified = true;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in entityValidationErrors.ValidationErrors)
                        {
                            System.Diagnostics.Debug.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                        }
                    }
                }
                return RedirectToAction("Login", "Account");
            }
        }


        [NonAction]
        public void SendEmailVerificationMail(User model)
        {
            var verifyUrl = "/Account/EmailVerification/" + model.ID.ToString();
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("darshsharma2810@gmail.com", "Darsh Sharma");
            var toMail = new MailAddress(model.EmailID);
            var fromEmailPassword = "fsdebzpgtgbmqzzh";
            string emailSubject = "Email Verification - Notes Marketplace";
            string emailBody = "Hello " + model.Firstname + "<br><br>Thank you for Signup. Please verify the email address via clicking on the link we sent you via email. <br><br>" + "<a href='" + link + "'>" + link + "</a><br><br>" + "Regards, <br>" + "Notes Marketplace";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toMail)
            {
                Subject = emailSubject,
                Body = emailBody,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}