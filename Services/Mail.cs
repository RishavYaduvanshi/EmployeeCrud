using System.Net;
using System.Net.Mail;

namespace EmployeeDetails.Services
{
    public class Mail
    {

        public bool SendEmail(string MailTo) 
        {
            string fromMail = "19gietucse196@gmail.com";
            string fromPassword = "bialtxrnwikfpxnx";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "ResetPassword";
 
            message.Body = $"<p>Hello,</p><p>Please click the following link to reset your password:</p><p><a href=\"http://localhost:4200/**?email={MailTo}\">RESETLINK</a></p>";
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
