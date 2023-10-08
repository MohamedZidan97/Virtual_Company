﻿using Microsoft.Extensions.Options;
using MimeKit;
using VirtualProject.BL.Interfaces;
using VirtualProject.Models;
using MailKit.Net.Smtp;

namespace VirtualProject.BL.Repositories
{
    public class MailingRep : IMailingRep
    {
        private readonly MailSettingOutlookVM mailSetting;

        public MailingRep(IOptions<MailSettingOutlookVM> MailSetting)
        {
            this.mailSetting = MailSetting.Value;
        }

        public async Task SendingMail(string mailTo, string subject, string body, IList<IFormFile> attechments = null)
        {
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(mailSetting.Email),
                Subject = subject,

            };

            email.To.Add(MailboxAddress.Parse(mailTo));
            // Body
            var builder = new BodyBuilder();
            if (attechments != null)
            {
                byte[] fileName;
                foreach (var file in attechments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        file.CopyTo(ms);

                        fileName = ms.ToArray();
                        builder.Attachments.Add(file.FileName, fileName, ContentType.Parse(file.ContentType));

                    }
                }
            }
            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(mailSetting.DisplayName, mailSetting.Email));


            using var smtp = new SmtpClient();

            smtp.Connect(mailSetting.Host, mailSetting.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSetting.Email, mailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
