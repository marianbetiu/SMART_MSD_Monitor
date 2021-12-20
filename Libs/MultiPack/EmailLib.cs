using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Net.Mime;

namespace MultiPack
{
    public class EmailLib
    {
        public static string Send(string host, string port, string encryption, string address, string name, string username, string password, string toAddressList, string ccAddressList, string replyToAddressList, string subject, string body, List<string> attachmentFileList)
        {
            string result = string.Empty;
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(address, name);

                    toAddressList = toAddressList.Replace(";", ",");
                    string[] toAddressListArray = toAddressList.Split(',');
                    foreach (var addr in toAddressListArray)
                    {
                        if (address.Trim() != "")
                        {
                            mail.To.Add(addr.Trim());
                        }
                    }

                    if (ccAddressList.Trim() != string.Empty)
                    {
                        ccAddressList = ccAddressList.Replace(";", ",");
                        string[] ccAddressListArray = ccAddressList.Split(',');
                        foreach (var addr in ccAddressListArray)
                        {
                            if (address.Trim() != "")
                            {
                                mail.CC.Add(addr.Trim());
                            }
                        }
                    }

                    if (replyToAddressList.Trim() != string.Empty)
                    {
                        replyToAddressList = replyToAddressList.Replace(";", ",");
                        string[] replyToAddressListArray = replyToAddressList.Split(',');
                        foreach (var addr in replyToAddressListArray)
                        {
                            if (address.Trim() != "")
                            {
                                mail.ReplyToList.Add(addr.Trim());
                            }
                        }
                    }

                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.Subject = subject;
                    mail.Body = body;

                    if ((attachmentFileList != null) && (attachmentFileList.Count > 0))
                    {
                        Attachment attachment;
                        foreach (var file in attachmentFileList)
                        {
                            if (File.Exists(file))
                            {

                                attachment = new Attachment(file, MediaTypeNames.Application.Octet);
                                attachment.NameEncoding = Encoding.UTF8;
                                attachment.TransferEncoding = TransferEncoding.Base64;
                                ContentDisposition disposition = attachment.ContentDisposition;
                                //disposition.CreationDate = File.GetCreationTime(file);
                                //disposition.ModificationDate = File.GetLastWriteTime(file);
                                //disposition.ReadDate = File.GetLastAccessTime(file);
                                disposition.FileName = Path.GetFileName(file);
                                disposition.Size = new FileInfo(file).Length;
                                disposition.DispositionType = DispositionTypeNames.Attachment;
                                mail.Attachments.Add(attachment);
                                
                            }
                        }
                    }

                    int portNumeric = 587;
                    int.TryParse(port, out portNumeric);

                    

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = host;
                        smtp.Port = portNumeric;
                        smtp.EnableSsl = encryption.ToUpper().Contains("SSL") || encryption.ToUpper().Contains("TLS");
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(username, password);
                        smtp.Timeout = 20000;

                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.ToString();
            }

            return result;
        }

        public static string Send(string host, string port, string encryption, string address, string name, string username, string password, string toAddressList, string subject, string body)
        {
            return Send(host, port, encryption, address, name, username, password, toAddressList, string.Empty, string.Empty, subject, body, null);
        }

        public static string Send(string host, string port, string encryption, string address, string name, string username, string password, string toAddressList, string subject, string body, List<string> attachmentFileList)
        {
            return Send(host, port, encryption, address, name, username, password, toAddressList, string.Empty, string.Empty, subject, body, attachmentFileList);
        }

        public static string SendContinental(List<string> toAddressList, List<string> ccAddressList, string subject, string body)
        {
            string error = string.Empty;

            try
            {
                MailMessage message = new MailMessage();
                if ((toAddressList != null) && (toAddressList.Count > 0))
                {
                    foreach (string toAddress in toAddressList)
                    {
                        message.To.Add(toAddress);
                    }
                }
                if ((ccAddressList != null) && (ccAddressList.Count > 0))
                {
                    foreach (string ccAddress in ccAddressList)
                    {
                        message.CC.Add(ccAddress);
                    }
                }
                message.Subject = subject;
                message.From = new MailAddress("LSW@continental-corporation.com", "Leader Standard Work - New user created!");
                message.IsBodyHtml = true;
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "tmlC001";
                smtp.Host = "smtphub07.conti.de";
                smtp.Timeout = 20000;
                smtp.Send(message);
            }
            catch (System.Exception ex)
            {
                error = ex.ToString();
            }

            return error;
        }

        public static string SendContinental(string toAddress, string ccAddress, string subject, string body)
        {
            string error = string.Empty;

            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(toAddress);
                if(ccAddress != null)
                    message.CC.Add(ccAddress);
                message.Subject = subject;
                message.From = new MailAddress("LSW@continental-corporation.com", "Leader Standard Work - New user created!");
                message.IsBodyHtml = true;
                message.Body = body;

                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "tmlC001";
                smtp.Host = "smtphub07.conti.de";
                smtp.Timeout = 20000;
                smtp.Send(message);
            }
            catch (System.Exception ex)
            {
                error = ex.ToString();
            }

            return error;
        }

        public static string SendContinental(string toAddress, string ccAddress, string subject, string body, string attachmentFile)
        {
            string error = string.Empty;

            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(toAddress);
                if (ccAddress != null)
                    message.CC.Add(ccAddress);
                message.Subject = subject;
                message.From = new MailAddress("LSW@continental-corporation.com", "Leader Standard Work - New user created!");

                //message.Body = body;

                LinkedResource LinkedImage = new LinkedResource(@attachmentFile);
                LinkedImage.ContentId = "MyPic";

                LinkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);
                var bodyContent = string.Format("{0}<br><img src=cid:MyPic>", body);
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(bodyContent,
                  null, "text/html");

                htmlView.LinkedResources.Add(LinkedImage);
                message.AlternateViews.Add(htmlView);

                message.IsBodyHtml = true;
                //message.Attachments.Add(att);

                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "tmlC001";
                smtp.Host = "smtphub07.conti.de";
                smtp.Timeout = 20000;
                smtp.Send(message);
            }
            catch (System.Exception ex)
            {
                error = ex.ToString();
            }

            return error;
        }

        public static string SendContinental(List<string> toAddressList, string subject, string body)
        {
            return SendContinental(toAddressList, null, subject, body);
        }

        public static string SendContinental(string toAddress, string subject, string body)
        {
            return SendContinental(toAddress, null, subject, body);
        }

        //public static string SendContinental(string toAddress, string subject, string body, string attachmentFile)
        //{
        //    return SendContinental(toAddress, null, subject, body, attachmentFile);
        //}
    }
}
