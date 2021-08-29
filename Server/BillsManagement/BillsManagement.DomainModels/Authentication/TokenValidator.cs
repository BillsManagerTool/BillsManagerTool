namespace BillsManagement.DomainModel
{
    public class TokenValidator
    {
        public DomainModel.SecurityToken SecurityToken { get; set; }

        public DomainModel.User Occupant { get; set; }
    }
}
