using LatihanLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace LatihanLogin.Controllers
{
    public class HomeController : Controller
    {
        latihanLoginEntities db = new latihanLoginEntities();
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveData(C_Login model)
        {
            model.IsValue = false;
            db.C_Login.Add(model);
            db.SaveChanges();
            BuildEmailTemplate(model.ID);
            return Json("Registration Succes", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Confirm(int regId)
        {
            ViewBag.regID = regId;
            return View();
        }
        public JsonResult RegisterConfirm(int regId)
        {
            C_Login data = db.C_Login.Where(a => a.ID == regId).FirstOrDefault();
            data.IsValue = true;
            db.SaveChanges();
            var msg = "Your email is verified";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public void BuildEmailTemplate(int regID)
        {
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/EmailTemplate/")+"Text"+".cshtml");
            var regInfo = db.C_Login.Where(x => x.ID == regID).FirstOrDefault();
            var url = "http://localhost:1642/" + "Home/Confirm?regID=" + regID;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();
            BuildEmailTemplate("Your email is verified", body,regInfo.Email);

        }

        public static void BuildEmailTemplate(string subjectText, string bodyText, string sendTo)
        {
            string from, to, bcc, cc, subject, body;
            from = "jujuk1020@gmail.com";
            to = sendTo.Trim();
            bcc = "";
            cc = "";
            subject = subjectText;
            StringBuilder sb = new StringBuilder();
            sb.Append(bodyText);
            body = sb.ToString();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(bcc))
            {
                mail.Bcc.Add(new MailAddress(bcc));
            }
            if (!string.IsNullOrEmpty(cc))
            {
                mail.CC.Add(new MailAddress(cc));
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            SendEmail(mail);

        }

        public static void SendEmail(MailMessage mail)
        {
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new System.Net.NetworkCredential("jujuk1020@gmail.com", "k4k1k4k1");
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        public JsonResult CheckValidUser(C_Login model)
        {
            string result = "Fail";
            var dataItem = db.C_Login.Where(x => x.Email == model.Email && x.Password == model.Password && x.IsValue==true).SingleOrDefault();
            if (dataItem!=null)
            {
                Session["UserID"] = dataItem.ID.ToString();
                Session["UserName"] = dataItem.Username.ToString();
                result = "Succes";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AfterLogin()
        {
            if ( Session["UserID"] !=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index"); 
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}