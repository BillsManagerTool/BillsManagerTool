using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Utility.Notifications
{
    public class EmailTemplate
    {
        public string RegisterNotificationTemplate()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<h4>Congratulations, your account has been successfully created.</h4>");
            sb.Append("</body></html>");

            return sb.ToString();
        }

        public string RegisterInvitationLinkTemplate(string registerLink)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><body>");
            sb.Append("<h3>You have been invited to join Bills Manager.</h3>");
            sb.Append($"<a href={registerLink}>Click here to register.</a>");
            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}
