using MengajiOneToOneSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MengajiOneToOneSystem.Controllers
{
    public class UserController : Controller
    {
        private object fromEmail;

        public object DefaultAuthenticationTypes { get; private set; }

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(MengajiOneToOneSystem.Models.User uModel)
        {
            using(db_motoEntities db = new db_motoEntities())
            {
                var userDetails = db.Users.Where(x => x.u_email == uModel.u_email && x.u_password == uModel.u_password).FirstOrDefault();
                Session["userID"] = userDetails.u_id;
                Session["userRole"] = userDetails.u_roles;
                Session["userName"] = userDetails.u_fname;
                Session["userLname"] = userDetails.u_lname;

                if (userDetails == null)
                {
                    uModel.LoginErrorMessage = " ID Pengguna atau Kata Laluan Tidak Sah";
                    return View("Login",uModel);
                }
                else
                {
                    if (userDetails.u_roles == "Admin")
                    {
                        
                        return RedirectToAction("Dashboard", "User");
                    }
                    else if (userDetails.u_roles == "Ustaz" || userDetails.u_roles == "Ustazah")
                    {
                        return RedirectToAction("TeacherDashboard", "User");
                    }
                    else
                    {
                        return RedirectToAction("Studentdashboard", "User");
                    }
                }
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult TeacherDashboard()
        {
            return View();
        }

        public ActionResult StudentDashboard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            
            Session.Remove("userID");
            Session.Remove("userNick");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("norhafiyzha13@gmail.com", "Dotnet Awesome");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "1_Assalamualaikum"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is successfully created!";
                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Reset Password";
                body = "Hi,<br/>br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);

        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //verify email id
            //Generate Reset password link
            //Send email
            string message = "";
            bool status =false;

            using(db_motoEntities db = new db_motoEntities())
            {
                var acc = db.Users.Where(x => x.u_email == EmailID).FirstOrDefault();
                if (acc != null)
                {
                    //Send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(acc.u_email, resetCode, "ResetPassword");
                    acc.u_passcode = resetCode;
                    //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                    //in our model class in part 1
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    message = "Reset password link has been sent to your email id.";

                }
                else
                {
                    message = "Account not found";
                }
            }


            return View();
        }

    }
}