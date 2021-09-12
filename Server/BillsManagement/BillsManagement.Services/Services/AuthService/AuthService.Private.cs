namespace BillsManagement.Services.Services.AuthService
{
    using BillsManagement.Exception.CustomExceptions;
    using BillsManagement.Services.ServiceContracts;
    using System.Net;
    using System.Net.Mail;
    using System.Text;

    public partial class AuthService : IAuthService
    {
        private void ValidateOccupantExistence(string email) // Think for a better method name
        {
            if (this._authRepository.IsExistingOccupant(email))
            {
                string msg = "Email is already used on another account"; // Think for a better message
                throw new HttpStatusCodeException(HttpStatusCode.Conflict, msg);
            }
        }

        private void SendRegisterNotificationEmail(string email, DomainModel.Settings settings)
        {
            using (MailMessage mail = new MailMessage())
            {
                var notificationMessage = this.CreateNotificationMessage();

                mail.From = new MailAddress(settings.BusinessEmail);
                mail.To.Add(email);
                mail.Subject = "Registration confirmed!";
                mail.Body = notificationMessage;
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(settings.BusinessEmail, settings.BusinessEmailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

        private string CreateNotificationMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}
