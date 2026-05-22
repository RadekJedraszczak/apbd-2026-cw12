namespace Hospital_Cf.DTOs;

public class GetBedAssignmentsDto
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public GetBedsDto Beds { get; set; }
}