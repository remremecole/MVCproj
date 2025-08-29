using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproj.Models;
using MVCproj.Services;


namespace MVCproj.Controllers
{
[Authorize]
public class PatientController : Controller
{
private readonly IPatientService _svc;
private readonly ILogger<PatientController> _logger;
public PatientController(IPatientService svc, ILogger<PatientController> logger)
{
_svc = svc; _logger = logger;
}


public async Task<IActionResult> Index(string? search)
{
ViewData["CurrentFilter"] = search;
var items = await _svc.GetAllAsync(search);
return View(items);
}


[HttpGet]
public IActionResult Create() => View();


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Patient p)
{
if (!ModelState.IsValid) return View(p);
try
{
await _svc.CreateAsync(p);
TempData["Success"] = "Patient créé avec succès";
return RedirectToAction(nameof(Index));
}
catch (Exception ex)
{
_logger.LogError(ex, "Erreur création patient");
ModelState.AddModelError(string.Empty, "Erreur lors de l'enregistrement");
return View(p);
}
}


[HttpGet]
public async Task<IActionResult> Edit(int id)
{
var p = await _svc.GetByIdAsync(id);
if (p == null) return NotFound();
return View(p);
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Patient p)
{
if (!ModelState.IsValid) return View(p);
await _svc.UpdateAsync(p);
TempData["Success"] = "Patient mis à jour";
return RedirectToAction(nameof(Index));
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Delete(int id)
{
await _svc.DeleteAsync(id);
TempData["Success"] = "Patient supprimé";
return RedirectToAction(nameof(Index));
}
}
}