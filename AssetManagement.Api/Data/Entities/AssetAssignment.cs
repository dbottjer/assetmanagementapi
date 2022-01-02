using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AssetManagement.Api.Data.Entities
{
    public partial class AssetAssignment
    {
        public int AssetAssignmentsId { get; set; }
        public int AssetId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateAssigned { get; set; }
        public int Term { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
