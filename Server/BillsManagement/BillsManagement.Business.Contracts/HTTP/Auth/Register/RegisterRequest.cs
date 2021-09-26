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
        public int TownId { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }

        [Required(ErrorMessage = "Building Address is required")]
        public string BuildingAddress { get; set; }

        [Required(ErrorMessage = "Entrance Number is required")]
        public string EntranceNumber { get; set; }
    }
}
