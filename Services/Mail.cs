using System.Net;
using System.Net.Mail;

namespace EmployeeDetails.Services
{
    public class Mail
    {

        public bool SendEmail(string MailTo,int body) 
        {
            string fromMail = "19gietucse196@gmail.com";
            string fromPassword = "bialtxrnwikfpxnx";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "ResetPassword";
            string mesReset = $"<p>Hello,</p><p>Please click the following link to reset your password:</p><p><a href=\"https://rishavemp.d3m0n1k.engineer//**?email={MailTo}\">RESETLINK</a></p>";
            string mesCnf = $"<p>Hello,</p><p>Please click the following link to verify your email address:</p><p><a href=\"http://rishav.d3m0n1k.engineer/email_verified?email={MailTo}\">VERIFYLINK</a></p>";
            if (body == 1)
            {
                message.Body = mesReset;
            }
            else
            {
                message.Body = mesCnf;
            }
            message.IsBodyHtml = true;
            message.To.Add(new MailAddress(MailTo));

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                return false;
            }

        }

    }
}
