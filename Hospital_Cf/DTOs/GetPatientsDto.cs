namespace Hospital_Cf.DTOs;

public class GetPatientsDto
{
    public string Pesel { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public bool Sex { get; set; }
    public ICollection<GetAdmissionDto> Admissions { get; set; } = [];
    public ICollection<GetBedAssignmentsDto> BedAssignments { get; set; } = [];
}