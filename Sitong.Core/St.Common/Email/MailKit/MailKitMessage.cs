using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace St.Common.Email.MailKit
{
    public class MailKitMessage : IMailKitMessage
    {

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> SendEmail(MimeMessage message)
        {
            try
            {
                var host = "smtp.exmail.qq.com";
                var port = 465;
                var useSsl = false;
                var from_username = "123@meowv.com";
                var from_password = "...";
                var from_name = "测试";
                var from_address = "123@meowv.com";

                var address = new List<MailboxAddress>
            {
                new MailboxAddress("111","111@meowv.com"),
                new MailboxAddress("222","222@meowv.com")
            };

                message.From.Add(new MailboxAddress(from_name, from_address));
                message.To.AddRange(address);

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(from_username, from_password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }

    }

    /// <summary>
    /// 演示代码：如何使用该项发送邮件
    /// </summary>
    internal class DemoEmail
    {
        private void TestSendEmail()
        {
            var builder = new BodyBuilder();
            var image = builder.LinkedResources.Add("图片地址");
            image.ContentId = MimeUtils.GenerateMessageId();
            builder.HtmlBody = $"当前时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss} <img src=\"cid:{image.ContentId}\"/>";
            var message = new MimeMessage
            {
                Subject = "我是邮件主题",
                Body = builder.ToMessageBody()
            };
        }
    }
}
