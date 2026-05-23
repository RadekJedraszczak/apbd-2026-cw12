namespace Hospital_Cf.DTOs;

public class GetRoomsDto
{
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public GetWardsDto Wards { get; set; } = null!;
}