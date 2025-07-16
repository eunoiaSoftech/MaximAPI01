using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Eunoia_UM_API.Model;

namespace Eunoia_UM_API.Helper
{
    public class DBCS
    {
        public static string GetConnectionString()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
           .Build();

            var str = configuration.GetSection("ConnectionString:DBCS").Value;
            return str;
        }
        public static string GetPANDocumentType()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
           .Build();

            var str = configuration.GetSection("DocumentTypeSettings:Pan").Value;
            return str;
        }
        public static string GetProfileDocumentType()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
           .Build();

            var str = configuration.GetSection("DocumentTypeSettings:ProfileImage").Value;
            return str;
        }

        public static string GetPaymentRecieptDocumentType()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
           .Build();

            var str = configuration.GetSection("DocumentTypeSettings:Payment").Value;
            return str;
        }
        public static string GetAadhaarDocumentType()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
           .Build();

            var str = configuration.GetSection("DocumentTypeSettings:Aadhaar").Value;
            return str;
        }

        #region Send Email Setting
        static string ESenderID = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["SenderID"];
        static string ESenderDisplayName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["SenderDisplayName"];
        static string ESenderIdPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["SenderIdPassword"];
        static string EURLToBeSend = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["URLToBeSend"];
        static string EURLToBeSendLogin = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["URLToBeSendLogin"];
        static string ESMTPHost = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["SMTPHost"];
        static string EPort = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SendEmailSettings")["Port"];
        #endregion

        public static SendEmail GetEmailSettings()
        {
            return new SendEmail
            {
                SenderID = ESenderID,
                SenderDisplayName = ESenderDisplayName,
                SenderIdPassword = ESenderIdPassword,
                SMTPHost = ESMTPHost,
                Port = Convert.ToInt32(EPort),
                URLToBeSend = EURLToBeSend,
                URLToBeSendLogin = EURLToBeSendLogin
            };

        }

        public static string SendMail(SendEmail email, string partyID = "")
        {
            try
            {
                var sendEmailSetting = GetEmailSettings();
                var senderEmail = new MailAddress(sendEmailSetting.SenderID, sendEmailSetting.SenderDisplayName);
                MailAddress receiverEmail = null;

                if (!string.IsNullOrWhiteSpace(email.RecieverEmailID))
                {
                    receiverEmail = !string.IsNullOrWhiteSpace(email.RecieverDisplayName)
                        ? new MailAddress(email.RecieverEmailID, email.RecieverDisplayName)
                        : new MailAddress(email.RecieverEmailID);
                }
                if (receiverEmail == null)
                {
                    return "Receiver email is not valid.";
                }
                var password = sendEmailSetting.SenderIdPassword;
                var sub = email.Subject;
                var text = "";
                if (email.Message == "Account Activation")
                {
                    sub = "Trail Period Activated";
                    text = "<div>Hello Dear " + email.RecieverDisplayName + ",<br/><br/>Your Account has been activated.<br/>Use below credentials to login<br/></div><br/>Click here to login :<a href='" + sendEmailSetting.URLToBeSendLogin + "'>Login Here</a> <br/><br/><table border=1 cellpadding=12 width=60%><tr><td>Username</td><td><b>" + email.Username + "</b></td></tr><tr><td>Password</td><td><b>" + email.Password + "</b></td></tr></table><br/><br/>Thanks,<br/> Team Smart Ensure";
                }
                if (email.Message == "FM Rate Changed Alert")
                {
                    sub = "Maxim FM Rate Change Alert 🚨";
                    text = "<div style='font-family: Arial, sans-serif; font-size: 14px; color: #333; background-color: #f9f9f9; padding: 20px;'>"
                            + "<div style='text-align: center; padding-bottom: 20px;'>"
                            + "</div>"
                            + "<h2 style='color: #d9534f;'>Freight Rate Change Notification 🚨</h2>"
                            + "<p style='font-size: 16px;'>Hello <strong>Dear User</strong>,</p>"
                            + "<p>It has been noticed that the <strong>Freight Rate</strong> has been changed while creating FM. Kindly find the updated details below:</p>"

                            // Highlighted Section with Table
                            + "<div style='background-color: #ffffff; border: 1px solid #ddd; padding: 20px; border-radius: 5px;'>"
                            + "<table cellpadding='10' cellspacing='0' style='width: 100%; border-collapse: collapse;'>"
                            + "<thead style='background-color: #d9edf7; color: #31708f;'>"
                            + "<tr><th style='border-bottom: 2px solid #ddd; text-align: left;'>Field</th>"
                            + "<th style='border-bottom: 2px solid #ddd; text-align: left;'>Details</th></tr>"
                            + "</thead>"
                            + "<tbody>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Entry Name</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.EntryNo + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Entry Date</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.EntryDt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Freight In The Name Of</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.FrghtInNmOf + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Broker/Owner Name</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.OwnBrkNm + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Booking Rate</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.FrtRt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Changed Rate</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.ChngdFrtRt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Reason</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.Rsn + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Confirmed By</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.CnfrmBy + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Created By</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.CreatedBy + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Changed On</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.ChngdOn + "</strong></td></tr>"
                            + "</tbody>"
                            + "</table>"
                            + "</div>"
                            + "<br/><p>Thanks,</p>"
                            + "<p>Team Maxim @EunoiaSofttech</p>"
                            + "</div>";
                }
                if (email.Message == "FM Receipt Rate Changed Alert")
                {
                    sub = "Maxim FM Receipt Rate Change Alert 🚨";
                    text = "<div style='font-family: Arial, sans-serif; font-size: 14px; color: #333; background-color: #f9f9f9; padding: 20px;'>"
                            + "<div style='text-align: center; padding-bottom: 20px;'>"
                            + "</div>"
                            + "<h2 style='color: #d9534f;'>Freight Receipt Rate Change Notification 🚨</h2>"
                            + "<p style='font-size: 16px;'>Hello <strong>Dear User</strong>,</p>"
                            + "<p>It has been noticed that the <strong>Freight Rate</strong> has been changed while creating FM Receipt. Kindly find the updated details below:</p>"

                            // Highlighted Section with Table
                            + "<div style='background-color: #ffffff; border: 1px solid #ddd; padding: 20px; border-radius: 5px;'>"
                            + "<table cellpadding='10' cellspacing='0' style='width: 100%; border-collapse: collapse;'>"
                            + "<thead style='background-color: #d9edf7; color: #31708f;'>"
                            + "<tr><th style='border-bottom: 2px solid #ddd; text-align: left;'>Field</th>"
                            + "<th style='border-bottom: 2px solid #ddd; text-align: left;'>Details</th></tr>"
                            + "</thead>"
                            + "<tbody>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Entry Name</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.EntryNo + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Entry Date</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.EntryDt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Freight In The Name Of</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.FrghtInNmOf + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Broker/Owner Name</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.OwnBrkNm + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Booking Rate</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.BkngRt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Addition Amount</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.AdtnAmnt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Waive Amount</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.WvAmnt + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Waive By</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.WavedBy + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Created By</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.CreatedBy + "</strong></td></tr>"
                            + "<tr><td style='border-bottom: 1px solid #ddd;'>Changed On</td><td style='border-bottom: 1px solid #ddd;'><strong>" + email.ChngdOn + "</strong></td></tr>"
                            + "</tbody>"
                            + "</table>"
                            + "</div>"
                            + "<br/><p>Thanks,</p>"
                            + "<p>Team Maxim @EunoiaSofttech</p>"
                            + "</div>";
                }
                if (email.Message == "Registration")
                {
                    sub = "New Registration";


                    //var TemplateData = userDL.GetEmailTemplate("NewRegistrationTemplate");
                    //UserDetailsData data = userDL.GetdetailsForUserforEmail(partyID);



                    ////using streamreader for reading my htmltemplate   
                    //using (StreamReader reader = new StreamReader(Path.Combine("EmailTemplates", "Registration.html")))
                    //{
                    //    text = reader.ReadToEnd();
                    //}

                    //if (data.userDetails != null)
                    //{
                    //    var serviceNames = "<ul>";
                    //    var hardwareNames = "<ul>";

                    //    var paymentMode = "";
                    //    var amount = "";
                    //    var paymentStatus = "";

                    //    foreach (var item in data.serviceName)
                    //    {
                    //        if (item.ServiceName != "" || !string.IsNullOrEmpty(item.ServiceName))
                    //            serviceNames += "<li>" + item.ServiceName + "</li>";
                    //    }
                    //    serviceNames += "</ul>";

                    //    foreach (var item in data.harwareName)
                    //    {
                    //        if (item.HardwareName != "" || !string.IsNullOrEmpty(item.HardwareName))
                    //            hardwareNames += "<li>" + item.HardwareName + "</li>";
                    //    }
                    //    hardwareNames += "</ul>";

                    //    foreach (var item in data.payDetails)
                    //    {
                    //        if (item.PaymentMode != "" || !string.IsNullOrEmpty(item.PaymentMode))
                    //        {
                    //            paymentMode = item.PaymentMode;
                    //            amount = "&#x20b9; " + item.Amount;
                    //            paymentStatus = item.PaymentStatus;
                    //        }
                    //    }

                    //    text = text.Replace("{{Name}}", data.userDetails.Name);
                    //    text = text.Replace("{{Mobile}}", data.userDetails.MobileNumber);
                    //    text = text.Replace("{{Email}}", data.userDetails.EmailId);
                    //    text = text.Replace("{{MobileVerify}}", data.userDetails.MobileVerified == 1 ? "Done" : "Pending");
                    //    text = text.Replace("{{EmailVerify}}", data.userDetails.EmailVerified == 1 ? "Done" : "Pending");
                    //    text = text.Replace("{{AadhaarVerify}}", data.userDetails.AdhaarVerified == 1 ? "Done" : "Pending");

                    //    text = text.Replace("{{MobileVerifycolor}}", data.userDetails.MobileVerified == 1 ? "green" : "red");
                    //    text = text.Replace("{{EmailVerifycolor}}", data.userDetails.EmailVerified == 1 ? "green" : "red");
                    //    text = text.Replace("{{AadhaarVerifycolor}}", data.userDetails.AdhaarVerified == 1 ? "green" : "red");


                    //    text = text.Replace("{{ServiceCollection}}", serviceNames);
                    //    text = text.Replace("{{HardwareCollection}}", hardwareNames);
                    //    text = text.Replace("{{PaymentMode}}", paymentMode);
                    //    text = text.Replace("{{PaymentAmount}}", amount);
                    //    text = text.Replace("{{PaymentStatus}}", paymentStatus);

                    //}

                    text = $"<div>Hello Dear {email.RecieverDisplayName}, <br/><br/>Your Account has been created and under varification soon your account will be activated be patient<br/></div><br/>Click here to login :<a href='{sendEmailSetting.URLToBeSendLogin}'>Login Here</a> <br/><br/><table border=1 cellpadding=12 width=60%><tr><td>Username</td><td><b>{email.Username}</b></td></tr><tr><td>Password</td><td><b>{email.Password}</b></td></tr></table><br/><br/>Thanks,<br/> Team Smart Ensure";

                }

                if (email.Message == "EmailOTP")
                {
                    sub = "Email Verification";
                    text = $"<div>Hello Dear {email.RecieverDisplayName}, <br/><br/>Your varification code : {email.emailOTP}, Please Verify.....<br/></div><br/><br/>Thanks,<br/> Team Smart Ensure";
                }
                //else
                //{
                //    text = "<div>Hello Dear " + email.RecieverDisplayName + ",<br/><br/>Please Find your username and password<br/></div><br/>Click here to Reset Password :<a href='" + sendEmailSetting.URLToBeSend + email.Mobile + "'>Reset Password</a> <br/><br/><table border=1 cellpadding=12 width=60%><tr><td>Username</td><td><b>" + email.Mobile + "</b></td></tr><tr><td>Password</td><td><b>" + email.Password + "</b></td></tr></table><br/><br/>Thanks,<br/> Team Zapurse";
                //}
                var body = text;
                var smtp = new SmtpClient
                {
                    Host = sendEmailSetting.SMTPHost,
                    Port = sendEmailSetting.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    // Add BCC and CC if they are valid
                    if (!string.IsNullOrWhiteSpace(email.RecieverCompnayHeadEmail))
                    {
                        mess.Bcc.Add(new MailAddress(email.RecieverCompnayHeadEmail));
                    }

                    if (!string.IsNullOrWhiteSpace(email.RecieverBranchHeadEmail))
                    {
                        mess.CC.Add(new MailAddress(email.RecieverBranchHeadEmail));
                    }

                    smtp.Send(mess);  // Send the email
                }
                return "Email Sent Successfully ...!!!";
            }
            catch (SmtpException smtpEx)
            {
                // Log the specific SMTP exception details
                return $"Failed to send email: {smtpEx.Message}";
            }

            catch (Exception ex)
            {
                // Catch and log other general exceptions
                return $"Error occurred: {ex.Message}";
            }
        }
        public static int GetOTP(int noOfDigits = 4)
        {
            Random rnd = new Random();
            Thread.Sleep(1500);
            return rnd.Next((int)Math.Pow(10, (noOfDigits - 1)), (int)Math.Pow(10, noOfDigits) - 1);
        }
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "KMDRE23870FDR3S";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "KMDRE23870FDR3S";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string Masked(string aadhaarNumber)
        {
            string cardNumber = aadhaarNumber;
            var pattern = "^(.{6})(.+)(.{4})$";
            var maskedNumber = Regex.Replace(cardNumber, pattern, (match) =>
            {
                return Regex.Replace(String.Format("{0}{1}{2}",
                new String('X', match.Groups[1].Value.Length), // the first 6 digits
                new String('X', match.Groups[2].Value.Length), // X times the 'X' char
                match.Groups[3].Value) /*the last 4 digits*/, ".{4}", "$0 "); //finally add a separator every 4 char
            });
            maskedNumber = maskedNumber.Replace(" ", "");
            return maskedNumber;
        }
        public static string SendMail_WithAttachment(NotificationEmail notification)
        {
            try
            {
                var sendEmailSetting = GetEmailSettings();
                var senderEmail = new MailAddress(sendEmailSetting.SenderID, sendEmailSetting.SenderDisplayName);
                var password = sendEmailSetting.SenderIdPassword;

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(senderEmail.Address);

                    // Add the To address
                    message.To.Add("iamvk249@gmail.com");
                    //message.To.Add(notification.To);

                    // Add multiple CC addresses
                    if (!string.IsNullOrEmpty(notification.CCEmailOnly))
                    {
                        var ccAddresses = notification.CCEmailOnly.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var cc in ccAddresses)
                        {
                            message.CC.Add(cc.Trim());
                        }
                    }

                    // Add multiple BCC addresses
                    if (!string.IsNullOrEmpty(notification.BCCEmailOnly))
                    {
                        var bccAddresses = notification.BCCEmailOnly.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var bcc in bccAddresses)
                        {
                            message.Bcc.Add(bcc.Trim());
                        }
                    }

                    // Set the subject and body
                    message.Subject = notification.Subject;
                    message.Body = notification.Body;
                    message.IsBodyHtml = true; // Set to false if body is plain text

                    // Add the attachment if it exists
                    if (!string.IsNullOrEmpty(notification.AttachmentBase64))
                    {
                        var attachmentBytes = Convert.FromBase64String(notification.AttachmentBase64);
                        var stream = new MemoryStream(attachmentBytes);
                        var attachment = new Attachment(stream, notification.AttachmentName + "." + notification.AttachmentBase64Ext);
                        message.Attachments.Add(attachment);
                    }


                    // Configure and send the email
                    var smtp = new SmtpClient
                    {
                        Host = sendEmailSetting.SMTPHost,
                        Port = sendEmailSetting.Port,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    smtp.Send(message);

                    return "Email sent successfully!";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }



            //try
            //{
            //    var sendEmailSetting = GetEmailSettings();
            //    var senderEmail = new MailAddress(sendEmailSetting.SenderID, sendEmailSetting.SenderDisplayName);
            //    var receiverEmail = new MailAddress(email.RecieverEmailID, email.RecieverDisplayName);
            //    var password = sendEmailSetting.SenderIdPassword;
            //    var sub = email.Subject;
            //    var text = "";

            //    var body = text;
            //    var smtp = new SmtpClient
            //    {
            //        Host = sendEmailSetting.SMTPHost,
            //        Port = sendEmailSetting.Port,
            //        EnableSsl = true,
            //        DeliveryMethod = SmtpDeliveryMethod.Network,
            //        UseDefaultCredentials = false,
            //        Credentials = new NetworkCredential(senderEmail.Address, password)
            //    };
            //    using (var mess = new MailMessage(senderEmail, receiverEmail)
            //    {
            //        Subject = sub,
            //        Body = body,
            //        IsBodyHtml = true
            //    })
            //    {
            //        smtp.Send(mess);
            //    }
            //    return "Email Sent";
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public static string SendPOMail(SendEmail email)
        {
            var sendEmailSetting = GetEmailSettings();
            var password = sendEmailSetting.SenderIdPassword;
            try
            {
                // Validate the essential fields
                if (string.IsNullOrWhiteSpace(email.ReceiverEmail))
                    return "Receiver email is not valid.";

                if (string.IsNullOrWhiteSpace(email.Message))
                    return "Email body cannot be empty.";

                if (string.IsNullOrWhiteSpace(email.Subject))
                    return "Email subject cannot be empty.";

                var senderEmail = new MailAddress(sendEmailSetting.SenderID, sendEmailSetting.SenderDisplayName);
                var receiverEmail = new MailAddress(email.ReceiverEmail, email.PartyName);

                if (receiverEmail == null)
                {
                    return "Receiver email is not valid.";
                }

                var smtp = new SmtpClient
                {
                    Host = sendEmailSetting.SMTPHost,
                    Port = sendEmailSetting.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                using (var message = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = email.Subject,
                    Body = email.Message,
                    IsBodyHtml = true
                })
                {
                    // Add CC recipients if provided and split by spaces
                    if (!string.IsNullOrWhiteSpace(email.CC))
                    {
                        var ccEmails = email.CC.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var ccEmail in ccEmails)
                        {
                            message.CC.Add(new MailAddress(ccEmail));
                        }
                    }

                    // Add BCC recipients if provided and split by spaces
                    if (!string.IsNullOrWhiteSpace(email.BCC))
                    {
                        var bccEmails = email.BCC.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var bccEmail in bccEmails)
                        {
                            message.Bcc.Add(new MailAddress(bccEmail));
                        }
                    }

                    smtp.Send(message); // Send the email
                }

                return "Email sent successfully!";
            }
            catch (SmtpException smtpEx)
            {
                return $"SMTP Error: {smtpEx.Message}";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
