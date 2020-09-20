using MimeKit;
using St.Common.Attributes;
using System.Threading.Tasks;

namespace St.Common.Email.MailKit
{
    [StDIInterface]
    public interface IMailKitMessage
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendEmail(MimeMessage message);
    }
}
