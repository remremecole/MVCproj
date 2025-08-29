using Microsoft.EntityFrameworkCore;
using MVCproj.Data;
using MVCproj.Models;


namespace MVCproj.Services
{
public class PatientService : IPatientService
{
private readonly MedManagerContext _ctx;
public PatientService(MedManagerContext ctx) => _ctx = ctx;


public async Task<IEnumerable<Patient>> GetAllAsync(string? search = null)
{
var q = _ctx.Patients.AsQueryable();
if (!string.IsNullOrWhiteSpace(search))
{
q = q.Where(p => p.Prenom.Contains(search)
|| (p.Adresse != null && p.Adresse.Contains(search))
|| (p.Ville != null && p.Ville.Contains(search))
|| p.NumeroSecu.Contains(search));
}
return await q.OrderByDescending(p => p.Id).ToListAsync();
}


public Task<Patient?> GetByIdAsync(int id) => _ctx.Patients.FindAsync(id).AsTask();


public async Task CreateAsync(Patient patient)
{
_ctx.Patients.Add(patient);
await _ctx.SaveChangesAsync();
}
public async Task UpdateAsync(Patient patient)
{
_ctx.Patients.Update(patient);
await _ctx.SaveChangesAsync();
}
public async Task DeleteAsync(int id)
{
var p = await _ctx.Patients.FindAsync(id);
if (p != null)
{
_ctx.Patients.Remove(p);
await _ctx.SaveChangesAsync();
}
}
}
}