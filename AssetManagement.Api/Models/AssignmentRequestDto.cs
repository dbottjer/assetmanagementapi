using System;
namespace AssetManagement.Api.Models
{
    public class AssignmentRequestDto
    {
        public int AssetAssignmentsId { get; set; }
        public int AssetId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateAssigned { get; set; }
        public int Term { get; set; }
    }
}
