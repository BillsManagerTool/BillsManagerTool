using System.ComponentModel.DataAnnotations;

namespace BillsManagement.Business.Contracts.HTTP
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Town is required")]
        public string Town { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Building Address is required")]
        public string BuildingAddress { get; set; }

        [Required(ErrorMessage = "Entrance Number is required")]
        public string EntranceNumber { get; set; }

        [Required(ErrorMessage = "Apartment Number is required")]
        public string ApartmentNumber { get; set; }

        [Required(ErrorMessage = "Apartment Floor is required")]
        public int ApartmentFloor { get; set; }
    }
}
