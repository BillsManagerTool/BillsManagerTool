using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillsManagement.Business.Contracts.HTTP
{
    public class RegisterOccupantRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Apartment Number is required")]
        public string ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Apartment Floor is required")]
        public int ApartmentFloor { get; set; }

        public string RegisterToken { get; set; }
    }
}
