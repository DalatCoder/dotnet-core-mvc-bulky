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

    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
        
        ProductViewModel productViewModel = new ProductViewModel();
        productViewModel.Product = new Product();
        productViewModel.CategoryList = categoryList;
        
        if (id != null && id > 0)
        {
            productViewModel.Product = _unitOfWork.Product.Get(u => u.Id == id);
            
            if (productViewModel.Product == null)
            {
                return NotFound();
            }
        }

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
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

        if (productViewModel.Product.Id == 0)
        {
            _unitOfWork.Product.Add(productViewModel.Product);
        }
        else
        {
            _unitOfWork.Product.Update(productViewModel.Product);
        }
        
        _unitOfWork.Save();
        TempData["success"] = "Product created/updated successfully";

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
