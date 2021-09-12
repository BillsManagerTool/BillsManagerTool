using BillsManagement.DomainModel;

namespace BillsManagement.Utility.Security
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(OccupantDetails user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
