using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;

namespace WebAppCom.Controllers
{
    public class BodegaController : Controller
    {
        private readonly AplicacionComercialContext context;

        public BodegaController(AplicacionComercialContext _context)
        {
            context = _context;
        }

        //-----------Administración de Bodega-----------------------------

        //GET: Departments List
        public async Task<IActionResult> _BodegaList()
        {

            ViewBag.bp = await context.BodegaProduct.Include(x => x.Bodega).Include(x => x.Product).ToListAsync();

            return View(await context.Bodega.ToListAsync());

        }

        // GET: TypeOfVisitor/partial/5
        // GET: TypeOfVisitor/Edit/5
        public IActionResult GetPartialBodega(int id = 0)
        {
            if (id == 0)
                return PartialView("_BodegaAddOrEdit", new Bodega());
            else
                return PartialView("_BodegaAddOrEdit", context.Bodega.Find(id));
        }

        // POST: TypeOfVisitor/Create
        // POST: TypeOfVisitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BodegaAddOrEdit(int id, Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (id == 0)
                    {
                        var dt = await context.Bodega.Where(x => x.Description == bodega.Description).FirstOrDefaultAsync();
                        if (dt != null)
                        {
                            ViewBag.dtMessage = "Este Departamento Ya existe";
                            return View(bodega);
                        }
                        context.Add(bodega);
                    }
                    else
                    {
                        context.Update(bodega);
                    }
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodegaExists(bodega.BodegaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(_BodegaList));
            }
            return View(bodega);
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> BodegaDelete(int? id)
        {
            var status = false;
            var bodega = await context.Bodega.FindAsync(id);
            var dt = context.BodegaProduct.Where(p => p.BodegaId == id).FirstOrDefault();
            if (dt != null)
            {
                status = false;
            }
            else if (bodega != null)
            {
                context.Bodega.Remove(bodega);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }

        //Confirm Exist
        private bool BodegaExists(int id)
        {
            return context.Bodega.Any(e => e.BodegaId == id);
        }

        //-----------Administración de BodegaProductos--------------------


        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> BodegaProductoProductoDelete(int bId, int pId)
        {
            var status = false;

            if (BodegaProductExists(bId, pId))
            {
                var res = context.BodegaProduct
                    .Where(e => e.ProductId == pId && e.BodegaId == bId)
                    .FirstOrDefault();

                if (res.Stock == 0)
                {
                    status = true;
                    context.BodegaProduct.Remove(res);
                    await context.SaveChangesAsync();
                    return Json(status);
                }
                
            }

            return Json(status);
        }


        private bool BodegaProductExists(int bodegaId, int productId)
        {
            var result = false;
            var res = context.BodegaProduct.Where(e => e.ProductId == productId && e.BodegaId == bodegaId).FirstOrDefault();
            if (res != null)
            {
                return true;
            }
            return result;
        }


    }
}
 