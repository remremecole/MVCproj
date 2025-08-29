using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCproj.Services;


namespace MVCproj.Controllers
{
[Authorize]
public class MedicamentController : Controller
{
private readonly IMedicamentService _svc;
public MedicamentController(IMedicamentService svc) => _svc = svc;


public async Task<IActionResult> Index(string? search)
{
var meds = await _svc.GetAllAsync(search);
return View(meds);
}
}
}