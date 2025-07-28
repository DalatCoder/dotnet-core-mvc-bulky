using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll().ToList();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _unitOfWork.Product.Add(product);
        _unitOfWork.Save();

        TempData["success"] = "Product created successfully";

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.Get(u => u.Id == id);

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _unitOfWork.Product.Update(product);
        _unitOfWork.Save();

        TempData["success"] = "Product updated successfully";

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Product? product = _unitOfWork.Product.Get(u => u.Id == id);

        return View(product);
    }

    [HttpPost]
    public IActionResult Delete(Product product)
    {
        Product? productToDelete = _unitOfWork.Product.Get(u => u.Id == product.Id);

        _unitOfWork.Product.Remove(productToDelete);
        _unitOfWork.Save();

        TempData["success"] = "Product deleted successfully";

        return RedirectToAction("Index");
    }
}
