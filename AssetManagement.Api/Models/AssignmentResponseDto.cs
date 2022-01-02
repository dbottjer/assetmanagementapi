using System;
namespace AssetManagement.Api.Models
{
    public class AssignmentResponseDto
    {
        public int AssetAssignmentsId { get; set; }
        public int AssetId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateAssigned { get; set; }
        public int Term { get; set; }
        public EmployeeDto Employee { get; set; }
        public AssetDto Asset { get; set; }

    }
}
