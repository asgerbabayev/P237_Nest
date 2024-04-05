using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P237_Nest.Data;
using P237_Nest.Extensions;
using P237_Nest.Models;

namespace P237_Nest.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]

public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.Products
                                               .Include(x => x.ProductImages)
                                               .Include(x => x.Category)
                                               .ToListAsync();
        return View(products);


    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Categories = await _context.Categories.ToListAsync();

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        //if (!ModelState.IsValid) return View(product);
        if (_context.Products.Any(p => p.Name == product.Name))
        {
            ModelState.AddModelError("", "Product already exists");
            return View(product);
        }
        product.ProductImages = new List<ProductImage>();
        if (product.Files != null)
        {
            foreach (var file in product.Files)
            {

                if (!file.CheckFileSize(2))
                {
                    ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                    return View(product);
                }


                if (!file.CheckFileType("image"))
                {
                    ModelState.AddModelError("Files", "Files must be image type!");
                    return View(product);
                }

                var filename = await file.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
                var additionalProductImages = CreateProduct(filename, false, false, product);

                product.ProductImages.Add(additionalProductImages);

            }
        }
        if (!product.MainFile.CheckFileSize(2))
        {
            ModelState.AddModelError("MainFile", "Files cannot be more than 2mb");
            return View(product);
        }


        if (!product.MainFile.CheckFileType("image"))
        {
            ModelState.AddModelError("MainFile", "Files must be image type!");
            return View(product);
        }

        var mainFileName = await product.MainFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
        var mainProductImageCreate = CreateProduct(mainFileName, false, true, product);

        product.ProductImages.Add(mainProductImageCreate);

        if (!product.HoverFile.CheckFileSize(2))
        {
            ModelState.AddModelError("HoverFile", "Files cannot be more than 2mb");
            return View(product);
        }


        if (!product.HoverFile.CheckFileType("image"))
        {
            ModelState.AddModelError("HoverFile", "Files must be image type!");
            return View(product);
        }

        var hoverFileName = await product.HoverFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
        var hoverProductImageCreate = CreateProduct(hoverFileName, true, false, product);
        product.ProductImages.Add(hoverProductImageCreate);



        await _context.Products.AddAsync(product);

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public ProductImage CreateProduct(string url, bool isHover, bool isMain, Product product)
    {
        return new ProductImage
        {
            Url = url,
            IsHover = isHover,
            IsMain = isMain,
            Product = product
        };
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || id < 1) return View("404");
        ViewBag.Categories = await _context.Categories.ToListAsync();
        var product = await _context.Products.Include(x => x.ProductImages)
                                             .Include(x => x.Category)
                                             .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) return View("404");


        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, Product product)
    {
        if (id != product.Id || id == null || id < 1) return BadRequest();

        var existProduct = await _context.Products.FindAsync(id);


        if (product.Files != null)
        {
            foreach (var file in product.Files)
            {

                if (!file.CheckFileSize(2))
                {
                    ModelState.AddModelError("Files", "Files cannot be more than 2mb");
                    return View(product);
                }


                if (!file.CheckFileType("image"))
                {
                    ModelState.AddModelError("Files", "Files must be image type!");
                    return View(product);
                }
                var filename = await file.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
                var additionalProductImages = CreateProduct(filename, false, false, product);
                existProduct.ProductImages.Add(additionalProductImages);
            }
        }
        if (product.MainFile != null)
        {
            if (!product.MainFile.CheckFileSize(2))
            {
                ModelState.AddModelError("MainFile", "Files cannot be more than 2mb");
                return View(product);
            }


            if (!product.MainFile.CheckFileType("image"))
            {
                ModelState.AddModelError("MainFile", "Files must be image type!");
                return View(product);
            }

            product.MainFile.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existProduct.ProductImages.FirstOrDefault(x => x.IsMain).Url);
            var mainFileName = await product.MainFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
            var mainProductImage = CreateProduct(mainFileName, false, false, product);
            existProduct.ProductImages.Add(mainProductImage);

        }
        if (product.HoverFile != null)
        {
            if (!product.HoverFile.CheckFileSize(2))
            {
                ModelState.AddModelError("HoverFile", "Files cannot be more than 2mb");
                return View(product);
            }


            if (!product.HoverFile.CheckFileType("image"))
            {
                ModelState.AddModelError("HoverFile", "Files must be image type!");
                return View(product);
            }

            product.HoverFile.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existProduct.ProductImages.FirstOrDefault(x => x.IsHover).Url);
            var hoverFileName = await product.HoverFile.SaveFileAsync(_env.WebRootPath, "client", "assets", "imgs/products");
            var hoverProductImageCreate = CreateProduct(hoverFileName, true, false, product);
            existProduct.ProductImages.Add(hoverProductImageCreate);
        }

        existProduct.Name = product.Name;
        existProduct.Description = product.Description;
        existProduct.SellPrice = product.SellPrice;
        existProduct.Rating = product.Rating;
        existProduct.DiscountPrice = product.DiscountPrice;
        existProduct.CategoryId = product.CategoryId;


        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || id <= 0) return BadRequest();
        var product = await _context.Products.Include(x => x.Category)
                                             .Include(x => x.ProductImages)
                                             .Include(x => x.ProductSizes)
                                             .ThenInclude(x => x.Size)
                                             .FirstOrDefaultAsync(x => x.Id == id);
        if (product == null) return NotFound();
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var existsImage = await _context.ProductImages.FindAsync(id);
        var product = await _context.Products.Include(x => x.Category)
                                              .Include(x => x.ProductImages)
                                              .FirstOrDefaultAsync(x => x.Id == existsImage.ProductId);
        existsImage.File.DeleteFile(_env.WebRootPath, "client", "assets", "imgs/products", existsImage.Url);
        _context.Remove(existsImage);
        await _context.SaveChangesAsync();
        return PartialView("_ProductImagePartial", product.ProductImages);
    }
}
