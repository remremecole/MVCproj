using MVCproj.Models;


namespace MVCproj.Services
{
public interface IPatientService
{
Task<IEnumerable<Patient>> GetAllAsync(string? search = null);
Task<Patient?> GetByIdAsync(int id);
Task CreateAsync(Patient patient);
Task UpdateAsync(Patient patient);
Task DeleteAsync(int id);
}
}