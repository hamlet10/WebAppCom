using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;
using WebAppCom.Utility;

namespace WebAppCom.Controllers
{
    public class ProductController : Controller
    {
        private readonly AplicacionComercialContext _context;

        public ProductController(AplicacionComercialContext context)
        {
            _context = context;
        }
        // GET: Product
        public async Task<IActionResult> Index(string department, string subcategory)
        {
            ViewBag.subtitle = department;
            ViewBag.restore = "d-none";
            ViewBag.buscar = "";

            var lst = (from p in _context.Product
                       .Include(p => p.Department).DefaultIfEmpty()
                       .Include(p => p.Iva).DefaultIfEmpty()
                       .Include(p => p.MeasureNavigation).DefaultIfEmpty()
                       .Include(p => p.ImagenPathArrs).DefaultIfEmpty()
                       select p).AsQueryable();

            if (!string.IsNullOrEmpty(department) && string.IsNullOrEmpty(subcategory))
            {
                lst = lst.Where(e => e.Department.Description.ToLower().Contains(department.ToLower()));

                ViewBag.buscar = "d-none";
                ViewBag.restore = "";
            }
            else if (!string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(subcategory))
            {
                lst = lst.Where(e => e.Department.Description.ToLower().Contains(department.ToLower()) &&
                                     e.Description.ToLower().Contains(subcategory.ToLower()));
                ViewBag.buscar = "d-none";
                ViewBag.restore = "";
            }
            else if (string.IsNullOrEmpty(department) && !string.IsNullOrEmpty(subcategory))
            {
                lst = lst.Where(e => e.Description.ToLower().Contains(subcategory.ToLower()));
                ViewBag.buscar = "d-none";
                ViewBag.restore = "";
            }

            return View(await lst.ToListAsync());
        }


        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Product
                .Include(p => p.Department)
                .Include(p => p.Iva)
                .Include(p => p.MeasureNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            Bar newBar = new Bar();
            var d = await _context.Bar.Where(b => b.ProductId == product.ProductId).FirstOrDefaultAsync();
            if (d != null)
            {
                ViewBag.valor = d.Bar1;
            }
            else
            {
                ViewBag.valor = null;
            }

            if (product == null)
            {
                return NotFound();
            }

            var Photos = await _context.ImagenPathArrs
                .Where(i => i.ProductId == product.ProductId)
                .ToListAsync();
            ViewBag.photos = Photos;

            ViewBag.photosCount = Photos.Count;

            if (Photos.Count == 0) ViewBag.EmptyImg = "hide";
            else if (Photos.Count > 0) ViewBag.EmptyImg = "show";


            ViewBag.Bodegas = await _context.BodegaProduct
                .Where(b => b.ProductId == product.ProductId)
                .Include(b => b.Bodega)
                .Include(b => b.Product)
                .ToListAsync();

            if (Photos.Count != 0) ViewBag.FirstPhoto = Photos[0].ImagePath;

            return View(product);
        }



        #region Nuevo Producto
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department.OrderBy(x => x.Description), "DepartmentId", "Description");
            ViewData["Ivaid"] = new SelectList(_context.Iva, "Ivaid", "Rate");
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "Description");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //Validando si el producto Existe.
                var pr = _context.Product
                    .Where(x => x.Description.ToLower() == product.Description.ToLower()).FirstOrDefault();
                if (pr != null)
                {
                    return Json(new { isValid = false, message = "El Producto Ya Existe", html = Utils.RenderRazorViewToString(this, "Create", product) });
                }

                if (product.Price <= 0)
                {
                    return Json(new { isValid = false, message = "El Precio debe ser mayor a 0", html = Utils.RenderRazorViewToString(this, "Create", product) });
                }

                if (file != null) product.ImagePath = await Utils.UploadFile(file);

                _context.Add(product);
                await _context.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Utils.RenderRazorViewToString(this, "Index", _context.Product
                           .Include(p => p.Department)
                           .Include(p => p.Measure)
                           .Include(p => p.Iva))
                });
            }

            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Description", product.DepartmentId);
            ViewData["Ivaid"] = new SelectList(_context.Iva, "Ivaid", "Description", product.Ivaid);
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "Description", product.MeasureId);
            return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "Create", product) });
        }

        #endregion

        #region Editar Producto
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department.OrderBy(x => x.Description), "DepartmentId", "Description", product.DepartmentId);
            ViewData["Ivaid"] = new SelectList(_context.Iva, "Ivaid", "Rate", product.Ivaid);
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "Description", product.MeasureId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile file)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        product.ImagePath = await Utils.UploadFile(file);
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "Description", product.DepartmentId);
            ViewData["Ivaid"] = new SelectList(_context.Iva, "Ivaid", "Description", product.Ivaid);
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "Description", product.MeasureId);
            return View(product);
        }
        #endregion

        #region Eliminar producto y validar existencia
        public async Task<IActionResult> Delete(int? id)
        {
            var status = false;
            var product = await _context.Product.FindAsync(id);
            try
            {
                var p = await _context.Product
                    .Where(x => x.PurchaseDetails.Count > 0 || x.OutPutDetails.Count > 0 || x.Kardex.Count > 0)
                    .ToListAsync();
                if (p.Count > 0)
                {
                    status = false;
                }

                var imgs = await _context.ImagenPathArrs.Where(x => x.ProductId == product.ProductId).ToListAsync();
                //Recorrer las imagenes del producto y eliminarla
                foreach (var item in imgs)
                {
                    ImagenDelete(item.ImagePath);
                }

                if (product != null)
                {
                    var deleteBar = _context.Bar.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                    var deletebp = _context.BodegaProduct.Where(x => x.ProductId == product.ProductId).ToList();
                    if (deletebp.Count > 0)
                    {
                        status = false;
                    }
                    else if (deleteBar != null)
                    {
                        status = false;
                    }
                    else
                    {
                        _context.Product.Remove(product);
                        await _context.SaveChangesAsync();
                        status = true;
                    }
                }

                return Json(status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex}");
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
        #endregion

        #region Codigo de Barra del Producto
        public async Task<IActionResult> AddBarCode(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBarCode(int id, string valor)
        {
            var product = await _context.Product.FindAsync(id);
            Bar newBar = new Bar();
            var d = await _context.Bar.Where(b => b.ProductId == product.ProductId).FirstOrDefaultAsync();
            try
            {
                if (d == null)
                {
                    newBar.ProductId = product.ProductId;
                    newBar.Bar1 = valor;
                    _context.Add(newBar);
                    await _context.SaveChangesAsync();
                    ViewBag.valor = valor;
                    return Json(new
                    {
                        isValid = true,
                        html = Utils.RenderRazorViewToString(this, "Index", _context.Product
                           .Include(p => p.Department)
                           .Include(p => p.Measure)
                           .Include(p => p.Iva))
                    });
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewBag.barcodereturn = valor;
            ViewBag.empMessenger = "Este producto ya tiene un Codigo de Barras: " + d.Bar1;
            return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddBarCode", product) });
        }
        #endregion

        #region Bodegas del Producto
        //Agregar Bodegas al Producto
        public async Task<IActionResult> AddBodega(int? id)
        {
            ViewData["BodegaId"] = new SelectList(_context.Bodega, "BodegaId", "Description");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "Description");
            ViewBag.bodegaProduct = new BodegaProduct();
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBodega(int id, int bodegaId, int stock,
            int min, int max, int makeUpDay, int minQuantity)
        {
            BodegaProduct pBodega = new BodegaProduct();
            var product = await _context.Product.FindAsync(id);

            if (!BodegaProductExists(bodegaId, id))
            {
                try
                {
                    pBodega.ProductId = product.ProductId;
                    pBodega.BodegaId = bodegaId;
                    pBodega.Stock = stock;
                    pBodega.Min = min;
                    pBodega.Max = max;
                    pBodega.MakeUpDay = makeUpDay;
                    pBodega.MinQuantity = minQuantity;
                    if (!BodegaProductBlank(pBodega))
                    {
                        ViewBag.bodegaProduct = pBodega;
                        return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddBodega", product) });
                    }
                    _context.BodegaProduct.Add(pBodega);
                    await _context.SaveChangesAsync();

                }
                catch (Exception)
                {
                    ViewBag.bodegaProduct = pBodega;
                    return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddBodega", product) });
                }
                return Json(new
                {
                    isValid = true,
                    html = Utils.RenderRazorViewToString(this, "Index", _context.Product
                           .Include(p => p.Department)
                           .Include(p => p.Measure)
                           .Include(p => p.Iva)
                           .ToListAsync())
                });
            }
            ViewBag.bodegaProduct = pBodega;
            ViewBag.qrMessenger = $"Esta Bodega ya esta asociada al Producto!";
            return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddBodega", product) });
        }

        //GET : Bodegas
        public async Task<IActionResult> GetAllBodegas()
        {
            var bodegas = await _context.BodegaProduct
                .Include(x => x.Bodega)
                .Include(x => x.Product)
                .ToListAsync();
            return View(bodegas);
        }

        //Valida si la bodega ya tiene el producto
        private bool BodegaProductExists(int bodegaId, int productId)
        {
            var result = false;
            var res = _context.BodegaProduct.Where(e => e.ProductId == productId && e.BodegaId == bodegaId).FirstOrDefault();
            if (res != null)
            {
                return true;
            }
            return result;
        }

        //Validacion sel formulario de BodegasProductos
        private bool BodegaProductBlank(BodegaProduct bodegaProduct)
        {
            if (bodegaProduct.BodegaId.Equals(0))
            {
                ViewBag.qrMessenger = $"El campo Bodega es obligatorio!";
                return false;
            }
            else if (bodegaProduct.Min.Equals(0))
            {
                ViewBag.qrMessenger = $"El campo Minimo es obligatorio!";
                return false;
            }
            else if (bodegaProduct.Max.Equals(0))
            {
                ViewBag.qrMessenger = $"El campo Maximo es obligatorio!";
                return false;
            }
            else if (bodegaProduct.MakeUpDay.Equals(0))
            {
                ViewBag.qrMessenger = $"El campo Día Reposición es obligatorio!";
                return false;
            }
            else if (bodegaProduct.MinQuantity.Equals(0))
            {
                ViewBag.qrMessenger = $"El campo Orden Minima es obligatorio!";
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Imagenes
        public async Task<IActionResult> AddImagenes(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImagenes(int id, IFormFile file)
        {
            var myImagen = await Utils.UploadFile(file);
            var product = await _context.Product.FindAsync(id);
            if (file == null)
            {
                return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddImagenes", product) });
            }
            var d = await _context.ImagenPathArrs.Where(b => b.ProductId == product.ProductId).ToListAsync();
            ImagenPathArrs imagen = new ImagenPathArrs();
            try
            {
                if (d.Count < 5)
                {
                    imagen.ProductId = product.ProductId;
                    imagen.ImagePath = myImagen;
                    _context.ImagenPathArrs.Add(imagen);

                    if (string.IsNullOrEmpty(product.ImagePath))
                    {
                        product.ImagePath = myImagen;
                        _context.Product.Update(product);
                    }

                    await _context.SaveChangesAsync();

                    return Json(new
                    {
                        isValid = true,
                        html = Utils.RenderRazorViewToString(this, "Index", _context.Product
                           .Include(p => p.Department)
                           .Include(p => p.Measure)
                           .Include(p => p.Iva)
                           .Include(p => p.ImagenPathArrs))
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Json(new { isValid = false, html = Utils.RenderRazorViewToString(this, "AddImagenes", product) });
        }

        public void ImagenDelete(string img)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Source", img);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        #endregion

    }
}