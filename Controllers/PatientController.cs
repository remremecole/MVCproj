using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCproj.Models;
using MVCproj.Services; // <-- pour IPatientService
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MVCproj.Controllers
{
    public class PatientController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;

        // Injection des dépendances
        public PatientController(IConfiguration configuration,
                                 IPatientService patientService,
                                 ILogger<PatientController> logger)
        {
            _configuration = configuration;
            _patientService = patientService;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var somVal = _configuration["MedManagerSettings:MaxPatientsPerPage"];
            ViewData["MaxPatientsPerPage"] = somVal;
            return View();
        }

        public IActionResult AjouterPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _patientService.CreateAsync(patient);
                    TempData["Success"] = "Patient ajouté avec succès";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de l'ajout du patient");
                    ModelState.AddModelError("", "Erreur lors de la sauvegarde");
                }
            }
            return View(patient);
        }
    }
}
