using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCproj.Models;

namespace MVCproj.Controllers;

public class HelloWorldController : Controller

{
    public IActionResult Index()
    {
        return View();
    }
}

