namespace BillsManagement.Security
{
    using BillsManagement.DomainModel;
    using System;

    public interface IJwtUtils
    {
        public string GenerateJwtToken(OccupantDetails user);
        public Guid? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
