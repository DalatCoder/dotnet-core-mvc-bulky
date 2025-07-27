using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public IActionResult Index()
    {
        List<Category> categories = _unitOfWork.Category.GetAll().ToList();
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
        
        _unitOfWork.Category.Add(category);
        _unitOfWork.Save();
        
        TempData["success"] = "Category created successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _unitOfWork.Category.Get(u => u.Id == id);

        return View(category);
    }
    
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        
        _unitOfWork.Category.Update(category);
        _unitOfWork.Save();
        
        TempData["success"] = "Category updated successfully";
        
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        
        Category? category = _unitOfWork.Category.Get(u => u.Id == id);

        return View(category);
    }
    
    [HttpPost]
    public IActionResult Delete(Category category)
    {
        Category? categoryToDelete = _unitOfWork.Category.Get(u => u.Id == category.Id);
        
        _unitOfWork.Category.Remove(categoryToDelete);
        _unitOfWork.Save();
        
        TempData["success"] = "Category deleted successfully";
        
        return RedirectToAction("Index");
    }
}