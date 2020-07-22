using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Angel.Utils
{
    /// <summary>
    /// 邮件发送类MailService
    /// </summary>
    public class MailService
    {
        public static MailMessage mailMessage = new MailMessage();
        public static string AttachmentPath;
        public List<Attachment> attchments;

        /// <summary>
        /// 发送邮件的方法
        /// </summary>
        /// <param name="toMail">目的邮件地址</param>
        /// <param name="title">发送邮件的标题</param>
        /// <param name="content">发送邮件的内容</param>
        /// <param name="CopytoMail">抄送邮件地址</param>
        /// <param name="FilePath">附件路径，附件之间以"|"相间隔</param>
        public void SendMail(string toMail, string title, string content, string CopyMail, string FilePath, bool isBodyHtml)
        {
            try
            {
                mailMessage.To.Clear();
                mailMessage.CC.Clear();
                mailMessage.AlternateViews.Clear();
                mailMessage.Headers.Clear();
                Encoding chtEnc = Encoding.BigEndianUnicode;//Encoding.UTF8
                AttachmentPath = FilePath;

                AddMailTo(toMail, chtEnc);

                AddCopyTo(CopyMail, chtEnc);

                //邮件标题编码
                mailMessage.SubjectEncoding = chtEnc;
                //邮件主题
                mailMessage.Subject = string.IsNullOrEmpty(title) ? "No Subject".ToString() : title.ToString();
                mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                //邮件内容
                mailMessage.Body = string.IsNullOrEmpty(content) ? "No Content".ToString() : content.ToString();
                //邮件内容编码
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.IsBodyHtml = isBodyHtml;//是否允许内容为 HTML 格式

                AddAttach(FilePath);//添加多个附件			

                //如果发送失败，SMTP 服务器将发送失败邮件告诉我
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                //发送邮件的优先等级（有效值为High,Low,Normal）
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                SmtpClient client = new SmtpClient();
                client.Timeout = 120000;
                //异步发送完成时的处理事件
                client.SendCompleted += new SendCompletedEventHandler(client_SendCompleted);
                //发送邮件
                client.Send(mailMessage); //同步发送
                //client.SendAsync(mailMessage, mailMessage.To); //异步发送 （异步发送时页面上要加上Async="true" ）
            }
            catch (Exception ep)
            {
                throw new ApplicationException(string.Format("发送邮件失败：\r\n {0}", ep.Message));
            }
        }

        /// <summary>
        /// 增加抄送人(支持多个)
        /// </summary>
        /// <param name="CopyMail">抄送人地址列表</param>
        /// <param name="chtEnc">编码</param>
        private void AddCopyTo(string CopyMail, Encoding chtEnc)
        {
            if (!string.IsNullOrEmpty(CopyMail))
            {
                string strCopyMail = CopyMail.Replace("；", ";");
                if (strCopyMail.Contains(";"))
                {
                    string[] mails = strCopyMail.Split(';');
                    foreach (string mail in mails)
                    {
                        if (!string.IsNullOrEmpty(mail))
                            mailMessage.CC.Add(new MailAddress(mail, mail.ToString(), chtEnc));
                    }
                }
                else
                {
                    mailMessage.CC.Add(new MailAddress(CopyMail, CopyMail.ToString(), chtEnc));
                }
            }

        }

        //<summary>
        //增加收件人(支持多个)
        //</summary>
        //<param name="toMail">收件人地址列表</param>
        //<param name="chtEnc">编码</param>
        private void AddMailTo(string toMail, Encoding chtEnc)
        {
            string strToMail = toMail.Replace("；", ";");
            if (strToMail.Contains(";"))
            {
                string[] mails = strToMail.Split(';');
                foreach (string mail in mails)
                {
                    if (!string.IsNullOrEmpty(mail))
                        mailMessage.To.Add(new MailAddress(mail, mail.ToString(), chtEnc));
                }
            }
            else
            {
                mailMessage.To.Add(new MailAddress(toMail, toMail.ToString(), chtEnc));
            }
        }


        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        private void AddAttach(string FilePath)
        {
            attchments = GetAttachByPath(FilePath);
            if (attchments.Count > 0)
            {
                if (mailMessage.Attachments.Count > 0)
                    mailMessage.Attachments.Clear();
                foreach (Attachment item in attchments)
                {
                    mailMessage.Attachments.Add(item);
                }
            }
        }

        /// <summary>
        /// 根据文件路径获得要添加的附件文件列表
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>附件文件列表</returns>
        private List<Attachment> GetAttachByPath(string FilePath)
        {
            List<Attachment> attachs = new List<Attachment>();
            //校验
            if (string.IsNullOrEmpty(FilePath)) { return new List<Attachment>(); }
            if (!FilePath.Contains("|")) { return new List<Attachment>(); }

            //去掉路径最后一位的"|"
            List<string> paths = GetPaths(ref FilePath);
            foreach (string path in paths)
            {
                attachs.Add(new Attachment(path, MediaTypeNames.Application.Octet));
            }
            return attachs;
        }

        /// <summary>
        /// 获得附件文件路径列表
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static List<string> GetPaths(ref string FilePath)
        {
            FilePath = FilePath.Remove(FilePath.Length - 1);
            string[] filepaths = FilePath.Split('|');
            List<string> paths = new List<string>();
            foreach (string path in filepaths)
            {
                paths.Add(path);
            }
            return paths;
        }

        //发送完毕后的事件
        void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                System.Web.HttpContext.Current.Response.Write("发送的邮件已被取消！");
            }
            if (e.Error == null)
            {
                System.Web.HttpContext.Current.Response.Write("邮件已成功发送！");
                ////释放资源
                //mailMessage.Dispose();
                mailMessage.Attachments.Clear(); //邮件发送完毕，释放对附件的锁定(这种方式会有问题,如果第二次发同一个文件,会报异常)
                DisposeAttchment();//邮件发送完毕，释放对附件的锁定
                //DeleteAttachFile();				
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write(e.Error.Message + " 原因是：" + e.Error.InnerException);
            }
        }



        /// <summary>
        /// 删除附件的源文件
        /// </summary>
        private static void DeleteAttachFile()
        {
            List<string> paths = GetPaths(ref AttachmentPath);
            foreach (string path in paths)
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
        }

        /// <summary>
        /// 邮件发送完毕，释放对附件的锁定
        /// </summary>
        private void DisposeAttchment()
        {
            if (attchments.Count > 0)
            {
                foreach (Attachment attach in attchments)
                {
                    attach.Dispose();
                }
            }

        }

    }
}
