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
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
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

        if (productViewModel.Product.ImageUrl == null)
        {
            productViewModel.Product.ImageUrl = "";
        }

        if (file != null)
        {
            if (!string.IsNullOrEmpty(productViewModel.Product.ImageUrl))
            {
                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            string upload = Path.Combine(wwwRootPath,  "images", "products");
            string extension = Path.GetExtension(file.FileName);
            
            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            
            productViewModel.Product.ImageUrl = Path.Combine("images", "products", fileName + extension);
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

    #region API Endpoints

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return Json(new { data = products });
    }

    #endregion
}