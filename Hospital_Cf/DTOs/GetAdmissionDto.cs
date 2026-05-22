

namespace Hospital_Cf.DTOs;

public class GetAdmissionDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public GetWardsDto Wards { get; set; } = null!;
}