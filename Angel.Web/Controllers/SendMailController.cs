using Angel.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Angel.Web.Controllers
{
    public class SendMailController : BaseController
    {
        //
        // GET: /SendMail/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult SendMail()
        {
            return Content("OK");
        }

        public ActionResult ZhengweiSendMail()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ZhengweiForSendMail()
        {
            string toMail = Request.Form["account"];
            string title = Request.Form["title"];
            string content = Request.Form["content"];
            //构建MailMessage对象
            SmtpSection smtp = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            MailAddress from = new MailAddress(smtp.From, "Angle通用权限管理系统");
            string filepath = "";
            MailService service = new MailService();
            service.SendMail(toMail, title, content, null, filepath, true);
             return Content("OK");
        }
    }
}
