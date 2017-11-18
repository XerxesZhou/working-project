using System;
using System.Text;
using System.Collections;
using System.Web.Mail;
using System.Configuration;

namespace SmartSoft.Component
{
    public class Mail
    {
        public Mail()
        { 
        
        }

        public string send_email(
            string to,
            string from,
            string cc,
            string subject,
            string body,
            MailFormat body_format,
            MailPriority priority,
            string[] attachments,
            bool return_receipt)
        {
            ArrayList files_to_delete = new ArrayList();
            MailMessage msg = new System.Web.Mail.MailMessage();
            msg.To = to;
            msg.From = from;
            msg.Cc = cc;
            msg.Subject = subject;
            msg.Priority = priority;

            // This fixes a bug for a couple people, but make it configurable, just in case.
            if (this.get_setting("BodyEncodingUTF8", "1") == "1")
            {
                msg.BodyEncoding = Encoding.GetEncoding("GB2312");
            }


            if (return_receipt)
            {
                msg.Headers.Add("Disposition-Notification-To", from);
            }

            // workaround for a bug I don't understand...
            if (this.get_setting("SmtpForceReplaceOfBareLineFeeds", "0") == "1")
            {
                body = body.Replace("\n", "\r\n");
            }

            msg.Body = body;
            msg.BodyFormat = body_format;


            string smtp_server = this.get_setting("SmtpServer", "");
            if (smtp_server != "")
            {
                System.Web.Mail.SmtpMail.SmtpServer = smtp_server;
            }

            string smtp_password = this.get_setting("SmtpServerAuthenticatePassword", "");

            if (smtp_password != "")
            {
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/sendpassword"] = smtp_password;
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpauthenticate"] = 1;
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/sendusername"] =
                    this.get_setting("SmtpServerAuthenticateUser", "");
            }

            string smtp_pickup = this.get_setting("SmtpServerPickupDirectory", "");
            if (smtp_pickup != "")
            {
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpserverpickupdirectory"] = smtp_pickup;
            }


            string send_using = this.get_setting("SmtpSendUsing", "");
            if (send_using != "")
            {
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/sendusing"] = send_using;
            }


            string smtp_use_ssl = this.get_setting("SmtpUseSSL", "");
            if (smtp_use_ssl == "1")
            {
                msg.Fields["http://schemas.microsoft.com/cdo/configuration/smtpusessl"] = "true";
            }


            string email_path = "";
            if (attachments != null)
            {
                foreach (string attachment in attachments)
                {
                    // remove the bug and attachment prefixes from the filename
                    string attachment_file_file = System.IO.Path.GetFileName(attachment);

                    int pos = attachment_file_file.IndexOf("_");
                    if (pos > -1)
                    {
                        pos = attachment_file_file.IndexOf("_", pos + 1);
                        if (pos > -1)
                        {
                            email_path = System.IO.Path.GetDirectoryName(attachment)
                                + "\\" + attachment_file_file.Substring(pos + 1);
                        }
                    }

                    
                    System.Web.Mail.MailAttachment mail_attachment;

                    // use the file without the attachment prefixes
                    if (email_path != "")
                    {

                        System.IO.File.Copy(attachment, email_path);
                        files_to_delete.Add(email_path);

                        mail_attachment = new System.Web.Mail.MailAttachment(
                            email_path,
                            System.Web.Mail.MailEncoding.Base64);
                        msg.Attachments.Add(mail_attachment);
                        // intentionally not deleting file here - see below
                    }
                    else
                    {
                        mail_attachment = new System.Web.Mail.MailAttachment(
                            attachment,
                            System.Web.Mail.MailEncoding.Base64);
                        msg.Attachments.Add(mail_attachment);
                    }

                }

            }


            try
            {
                System.Web.Mail.SmtpMail.Send(msg);

                // We delete late here because testing showed that SmtpMail class
                // got confused when we deleted too soon.
                if (files_to_delete.Count > 0)
                {
                    foreach (string file in files_to_delete)
                    {
                        System.IO.File.Delete(file);
                    }
                }

                return "";
            }
            catch (Exception e)
            {
                return (e.GetBaseException().Message);
            }

        }


        public string send_email(
            string to,
            string from,
            string cc,
            string subject,
            string body)
        {
            return send_email(
                to,
                from,
                cc,
                subject,
                body,
                System.Web.Mail.MailFormat.Text,
                System.Web.Mail.MailPriority.Normal,
                null,
                false);
        }


        public string get_setting(string key, string defaultvalue)
        {
            try
            {
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    return ConfigurationManager.AppSettings[key].ToString();
                }
                else
                {
                    return defaultvalue;
                }
            }
            catch
            {
                return defaultvalue;
            }
        }
    }
}
