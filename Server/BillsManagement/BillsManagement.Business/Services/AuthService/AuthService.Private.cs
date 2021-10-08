namespace BillsManagement.Business.Services.AuthService
{
    using BillsManagement.Business.Contracts.ServiceContracts;
    using BillsManagement.Custom.CustomExceptions;
    using BillsManagement.DomainModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
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

        private void SendEmailNotification(string receiver, string subject, string body)
        {
            var settings = this._authRepository.GetNotificationSettings(1);

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(settings.BusinessEmail);
                mail.To.Add(receiver);
                mail.Subject = subject;
                mail.Body = body;
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

        private void RevokeDescendantRefreshTokens(RefreshToken refreshToken, OccupantDetails occupantDetails, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = this._authRepository.GetChildToken(refreshToken);
                if (childToken.IsActive)
                    this.RevokeRefreshToken(childToken, ipAddress, reason);
                else
                    this.RevokeDescendantRefreshTokens(childToken, occupantDetails, ipAddress, reason);
            }
        }

        private void RevokeRefreshToken(RefreshToken token, string ipAddress, string reason, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;

            this._authRepository.RevokeRefreshToken(token);
        }

        private RefreshToken RotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            this.RevokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        // Move email templates in util class
        private string CreateNotificationMessage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<h3>Thank you for joining the Bills Management beta.</h3>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        private string BuildRegisterQueryString(string token)
        {
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);

            queryString.Add("RegisterToken", token);

            return queryString.ToString();
        }

        private string BuildRegisterLink(string tokenQueryParam, string clientUrl)
        {
            var registerLink = $"{clientUrl}/Auth/RegisterOccupant?{tokenQueryParam}";

            return registerLink;
        }
    }
}
