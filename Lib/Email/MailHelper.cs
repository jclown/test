using MimeKit;
using MimeKit.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Lib
{
    /// <summary>  
    /// 发送邮件的类  
    /// </summary>  
    public class MailHelper
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public List<dynamic> Attachments { get; set; }

        private static string ConvertToBase64(string inputStr, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(inputStr));
        }

        private static string ConvertHeaderToBase64(string inputStr, Encoding encoding)
        {
            var encode = !string.IsNullOrEmpty(inputStr) && inputStr.Any(c => c > 127);
            if (encode)
            {
                return "=?" + encoding.WebName + "?B?" + ConvertToBase64(inputStr, encoding) + "?=";
            }
            return inputStr;
        }

        public void Send(string from, string password, string to, string content, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(this.Host))
                {
                    this.Host = "smtp." + from.Split('@')[1];
                }
                if (this.Port == 0)
                {
                    this.Port = 25;
                }

                SendByMailKit(from, password, to, content, title);
            }
            catch (Exception ex)
            {
                try
                {
                    SendByNetstandard(from, password, to, content, title);
                }
                catch
                {
                    throw new Exception("邮件发送失败，已尝试2次发送。", ex);
                }
            }
        }

        /// <summary>
        /// MailKit组件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="password"></param>
        /// <param name="to"></param>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private void SendByMailKit(string from, string password, string to, string content, string title)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(from, from));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = title;

            //message.Body = new TextPart(TextFormat.Html) { Text = content };
            var body = new TextPart(TextFormat.Html) { Text = content };
            MimeEntity entity = body;

            if (this.Attachments != null)
            {
                var mult = new Multipart("mixed") { body };
                foreach (var att in this.Attachments)
                {
                    if (att.Data != null)
                    {
                        var attachment = string.IsNullOrWhiteSpace(att.ContentType) ? new MimePart() : new MimePart(att.ContentType);
                        attachment.Content = new MimeContent(att.Data);
                        attachment.ContentDisposition = new ContentDisposition(ContentDisposition.Attachment);
                        //attachment.ContentTransferEncoding = att.ContentTransferEncoding;
                        //public ContentEncoding ContentTransferEncoding { get; set; } = ContentEncoding.Default;
                        attachment.FileName = ConvertHeaderToBase64(att.FileName, Encoding.UTF8);//解决附件中文名问题
                        mult.Add(attachment);
                    }
                }
                entity = mult;
            }
            message.Body = entity;

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Timeout = 30000;
                client.CheckCertificateRevocation = false;

                // Smtp服务器                
                client.Connect(this.Host, this.Port, false);

                // 登录: only needed if the SMTP server requires authenticationn
                client.Authenticate(from, password);

                // 发送
                client.Send(message);

                // 断开
                client.Disconnect(true);
            }
        }

        /// <summary>
        /// netcore自带的组件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="password"></param>
        /// <param name="to"></param>
        /// <param name="content"></param>
        /// <param name="title"></param>
        private void SendByNetstandard(string from, string password, string to, string content, string title)
        {
            var mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.From = new System.Net.Mail.MailAddress(from);
            mailMessage.Subject = title;
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;

            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
            smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            smtpClient.Host = this.Host;
            smtpClient.Port = this.Port;
            smtpClient.Send(mailMessage);
        }
    }
}
