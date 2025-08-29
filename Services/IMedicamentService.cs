using MVCproj.Models;


namespace MVCproj.Services
{
public interface IMedicamentService
{
Task<IEnumerable<Medicament>> GetAllAsync(string? search = null);
}
}