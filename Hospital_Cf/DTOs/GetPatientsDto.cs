namespace Hospital_Cf.DTOs;

public class GetPatientsDto
{
    public string Pesel { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public bool Sex { get; set; }
    public List<GetAdmissionDto> Admissions { get; set; } = [];
    public List<GetBedAssignmentsDto> BedAssignments { get; set; } = [];
}