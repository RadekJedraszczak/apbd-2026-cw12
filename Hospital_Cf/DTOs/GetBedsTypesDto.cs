namespace Hospital_Cf.DTOs;

public class GetBedsTypesDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}