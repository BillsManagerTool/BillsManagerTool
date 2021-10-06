namespace BillsManagement.Security
{
    using BillsManagement.DomainModel;
    public interface IJwtUtils
    {
        public string GenerateJwtToken(OccupantDetails user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
