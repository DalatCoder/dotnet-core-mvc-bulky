using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // GET
    public IActionResult Index()
    {
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
    }
}