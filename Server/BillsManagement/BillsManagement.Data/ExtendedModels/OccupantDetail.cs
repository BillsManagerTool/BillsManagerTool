namespace BillsManagement.Data.Models
{
    public partial class OccupantDetail
    {
        public string FullName 
        {
            get
            {
                return $"{this.FirstName} {this.LastName}";
            }
        }
    }
}
