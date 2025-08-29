using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproj.Models;
using MVCproj.Services;


namespace MVCproj.Controllers
{
[Authorize]
public class OrdonnanceController : Controller
{
private readonly IOrdonnanceService _svc;
private readonly IMedicamentService _meds;
private readonly IPatientService _patients;
private readonly ILogger<OrdonnanceController> _logger;


public OrdonnanceController(IOrdonnanceService svc, IMedicamentService meds, IPatientService patients, ILogger<OrdonnanceController> logger)
{ _svc = svc; _meds = meds; _patients = patients; _logger = logger; }


[HttpGet]
public async Task<IActionResult> Create()
{
ViewBag.Medicaments = await _meds.GetAllAsync();
ViewBag.Patients = await _patients.GetAllAsync();
return View(new Ordonnance { DateDebut = DateTime.Today, DateFin = DateTime.Today.AddDays(7) });
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Ordonnance ord, int[] medicamentIds, string[] qtes, string[] posologies)
{
if (!ModelState.IsValid)
{
ViewBag.Medicaments = await _meds.GetAllAsync();
ViewBag.Patients = await _patients.GetAllAsync();
return View(ord);
}
try
{
var lignes = medicamentIds.Select((id, i) => (id, qtes[i], posologies[i]));
var created = await _svc.CreerAsync(ord, lignes);
TempData["Success"] = "Ordonnance créée";
return RedirectToAction(nameof(Details), new { id = created.Id });
}
catch (IatrogeneException iex)
{
ModelState.AddModelError(string.Empty, iex.Message);
}
catch (Exception ex)
{
_logger.LogError(ex, "Erreur création ordonnance");
ModelState.AddModelError(string.Empty, "Erreur lors de l'enregistrement");
}
ViewBag.Medicaments = await _meds.GetAllAsync();
ViewBag.Patients = await _patients.GetAllAsync();
return View(ord);
}


public async Task<IActionResult> Details(int id)
{
var pdf = await _svc.GenererPdfAsync(id);
return File(pdf, "application/pdf", $"ordonnance_{id}.pdf");
}
}
}