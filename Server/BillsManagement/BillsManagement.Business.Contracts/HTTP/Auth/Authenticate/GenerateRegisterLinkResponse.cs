using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Business.Contracts.HTTP.Auth.Authenticate
{
    public class GenerateRegisterLinkResponse : BaseResponse
    {
        public string RegisterLink { get; set; }
    }
}
