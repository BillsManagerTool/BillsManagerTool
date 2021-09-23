namespace BillsManagement.Business.Services.AuthService
{
    using BillsManagement.Business.Contracts.HTTP;
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.DomainModel;
    using System;
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

        private void RemoveOldRefreshTokens(OccupantDetails occupantDetails)
            // remove old inactive refresh tokens from user based on TTL in app settings
            => this._authRepository.RemoveOldRefreshTokens(occupantDetails.OccupantDetailsId);

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, OccupantDetails occupantDetails, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = this._authRepository.GetChildToken(refreshToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, occupantDetails, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
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
