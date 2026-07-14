using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Reference.CustomLibrary
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public sealed class MailProcessor
    {
        #region 私有变量

        private MailMessage mailMessage;
        private SmtpClient smtpClient;
        private string smtpAddress;
        private string mailPassword;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="smtpAddress"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="password"></param>
        public MailProcessor(string smtpAddress, string from, string to, string subject, string body, string password)
        {
            this.smtpAddress = smtpAddress;
            mailMessage = new MailMessage(from, to, subject, body);
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = System.Net.Mail.MailPriority.High;
            this.mailPassword = password;
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="smtpAddress"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="password"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        public MailProcessor(string smtpAddress, string from, string to, string subject, string body, string password, string[] cc, string[] bcc)
            : this(smtpAddress, from, to, subject, body, password)
        {
            if (cc != null && cc.Length > 0)
            {
                foreach (string ccAddress in cc)
                {
                    mailMessage.CC.Add(new MailAddress(ccAddress));
                }
            } if (bcc != null && bcc.Length > 0)
            {
                foreach (string bccAddress in bcc)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                }
            }
        }

        #endregion

        #region 公有方法

        /// <summary>   
        /// 添加附件   
        /// </summary>   
        public void Attachments(string Path)
        {
            string[] path = Path.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化附件   
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的创建日期   
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);// 获取附件的修改日期   
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的读取日期   
                mailMessage.Attachments.Add(data);//添加到附件中   
            }
        }

        /// <summary>   
        /// 异步发送邮件   
        /// </summary>   
        /// <param name="CompletedMethod"></param>   
        public void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, mailPassword);//设置发件人身份的票据   
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.SendCompleted += new SendCompletedEventHandler(CompletedMethod);//注册异步发送邮件完成时的事件   
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
            }
        }

        /// <summary>   
        /// 发送邮件   
        /// </summary>   
        public void Send()
        {
            try
            {
                if (mailMessage != null)
                {
                    smtpClient = new SmtpClient();
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, mailPassword);//设置发件人身份的票据   
                    smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //smtpClient.Host = "smtp." + mailMessage.From.Host;
                    smtpClient.Host = smtpAddress;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

