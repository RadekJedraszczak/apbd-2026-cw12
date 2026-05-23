using Hospital_Cf.Models;

namespace Hospital_Cf.DTOs;

public class AddBedAssignmentDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public BedType BedType { get; set; } = null!;
    public Ward Ward { get; set; } = null!;
}