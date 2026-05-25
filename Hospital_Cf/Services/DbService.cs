using System.Globalization;
using Hospital_Cf.Data;
using Hospital_Cf.DTOs;
using Hospital_Cf.Exceptions;
using Hospital_Cf.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Cf.Services;

public class DbService : IDbService
{
    private readonly HospitalDfContext _context;

    public DbService(HospitalDfContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPatientsDto>> GetAllPatients(string? search)
    {
        var query = _context.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p =>
                EF.Functions.Like(p.FirstName, $"%{search}%") ||
                EF.Functions.Like(p.LastName, $"%{search}%"));
        }
        
        var res = await query
            .Select(p => new GetPatientsDto()
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex,
                Admissions = p.Admissions.Select(a => new GetAdmissionDto()
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate = a.DischargeDate,
                    Wards = new GetWardsDto()
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description
                    }
                }).ToList(),
                BedAssignments = p.BedAssignments.Select(ba => new GetBedAssignmentsDto()
                {
                    Id = ba.Id,
                    From = ba.From,
                    To = ba.To,
                    Beds = new GetBedsDto()
                    {
                        Id = ba.Bed.Id,
                        Type = new GetBedsTypesDto()
                        {
                            Id = ba.Bed.BedType.Id,
                            Name = ba.Bed.BedType.Name,
                            Description = ba.Bed.BedType.Description
                        }
                    },
                    Rooms = new GetRoomsDto()
                    {
                        Id = ba.Bed.Room.Id,
                        HasTv = ba.Bed.Room.HasTv,
                        Wards = new GetWardsDto()
                        {
                            Id = ba.Bed.Room.Ward.Id,
                            Name = ba.Bed.Room.Ward.Name,
                            Description = ba.Bed.Room.Ward.Description
                        }
                    }
                }).ToList(),
            }).ToListAsync();
            

        if (res == null)
        {
            throw new NotFoundException();
        }

        return res;
    }

    public async Task AddBedAssignment(AddBedAssignmentDto addBedAssignmentDto, string pesel)
    {
        var anyPatient = await _context.Patients.AnyAsync(p => p.Pesel ==  pesel);
        if (!anyPatient)
        {
            throw new NotFoundException();
        }
        
        var freeBed = await _context.Beds
            .Where(b => b.BedType == addBedAssignmentDto.BedType)
            .Where(b => b.Room.Ward == addBedAssignmentDto.Ward)
            .Where(b => !b.BedAssignments.Any(ba => 
                ba.From <= addBedAssignmentDto.To
                && (ba.To == null || ba.To >= addBedAssignmentDto.From)
                ))
            .FirstOrDefaultAsync();

        if (freeBed == null)
        {
            throw new NotFoundException();
        }
        
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var bedAssignment = new BedAssignment()
            {
                From = addBedAssignmentDto.From,
                To = addBedAssignmentDto.To,
                PatientPesel = pesel,
                BedId = freeBed.Id,
            };
            
            _context.BedAssignments.Add(bedAssignment);
            await _context.SaveChangesAsync();
            
            await transaction.CommitAsync();
        }catch(Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}