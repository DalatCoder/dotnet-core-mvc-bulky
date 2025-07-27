using Bulky.DataAccess.Data;
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
    
    public IActionResult Index()
    {
        List<Category> categories = _db.Categories.ToList();
        return View(categories);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        
        _db.Categories.Add(category);
        _db.SaveChanges();
        
        TempData["success"] = "Category created successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }
    
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        
        _db.Categories.Update(category);
        _db.SaveChanges();
        
        TempData["success"] = "Category updated successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }
    
    [HttpPost]
    public IActionResult Delete(Category category)
    {
        Category? categoryToDelete = _db.Categories.FirstOrDefault(u => u.Id == category.Id);
        if (categoryToDelete == null)
        {
            return NotFound();
        }
        
        _db.Categories.Remove(categoryToDelete);
        _db.SaveChanges();
        
        TempData["success"] = "Category deleted successfully";
        
        return RedirectToAction("Index");
    }
}