using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        ViewBag.CategoryList = categoryList;
        
        ProductViewModel productViewModel = new ProductViewModel();
        productViewModel.Product = new Product();
        productViewModel.CategoryList = categoryList;

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Create(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid)
        {
            productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            
            return View(productViewModel);
        }

        _unitOfWork.Product.Add(productViewModel.Product);
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
