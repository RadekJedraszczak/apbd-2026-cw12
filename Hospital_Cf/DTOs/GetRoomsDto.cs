namespace Hospital_Cf.DTOs;

public class GetRoomsDto
{
    public int Id { get; set; }
    public bool HasTv { get; set; }
    public GetWardsDto Wards { get; set; } = null!;
}