using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    
    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public IActionResult Index()
    {
        List<Category> categories = _categoryRepository.GetAll().ToList();
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
        
        _categoryRepository.Add(category);
        _categoryRepository.Save();
        
        TempData["success"] = "Category created successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _categoryRepository.Get(u => u.Id == id);

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
        
        _categoryRepository.Update(category);
        _categoryRepository.Save();
        
        TempData["success"] = "Category updated successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _categoryRepository.Get(u => u.Id == id);

        if (category == null)
        {
            return NotFound();
        }
        
        return View(category);
    }
    
    [HttpPost]
    public IActionResult Delete(Category category)
    {
        Category? categoryToDelete = _categoryRepository.Get(u => u.Id == category.Id);
        if (categoryToDelete == null)
        {
            return NotFound();
        }
        
        _categoryRepository.Remove(categoryToDelete);
        _categoryRepository.Save();
        
        TempData["success"] = "Category deleted successfully";
        
        return RedirectToAction("Index");
    }
}