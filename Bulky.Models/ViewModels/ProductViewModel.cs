using Bulky.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Models.ViewModels;

public class ProductViewModel
{
    public Product Product { get; set; }
    
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}