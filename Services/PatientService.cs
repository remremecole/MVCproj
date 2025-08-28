using Microsoft.EntityFrameworkCore;
using MVCproj.Models;
using MVCproj.Data;

namespace MVCproj.Services
{
    public class PatientService : IPatientService
    {
        private readonly MedManagerContext _context;

        public PatientService(MedManagerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
            => await _context.Patients.ToListAsync();

        public async Task<IEnumerable<Patient>> SearchAsync(string search)
            => await _context.Patients
                .Where(p => p.Prenom.Contains(search)
                         || (p.Adresse != null && p.Adresse.Contains(search))
                         || (p.Ville != null && p.Ville.Contains(search))
                         || (p.NumeroSecu != null && p.NumeroSecu.Contains(search)))
                .ToListAsync();

        public async Task<Patient?> GetByIdAsync(int id)
            => await _context.Patients.FindAsync(id);

        public async Task CreateAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
