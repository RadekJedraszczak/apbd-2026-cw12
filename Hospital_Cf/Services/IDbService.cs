

using Hospital_Cf.DTOs;

namespace Hospital_Cf.Services;

public interface IDbService
{
   Task<IEnumerable<GetPatientsDto>> GetAllPatients(string? search);
   Task AddBedAssignment(AddBedAssignmentDto addBedAssignmentDto, string pesel);
}