using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;
using WebAppCom.Utility;

namespace WebAppCom.Controllers
{
    public class IVAController : Controller
    {
        private AplicacionComercialContext context;

        public IVAController(AplicacionComercialContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await context.Iva.ToListAsync());
        }

        // GET: TypeOfVisitor/partial/5
        // GET: TypeOfVisitor/Edit/5
        public IActionResult GetPartialIva(int id = 0)
        {
            if (id == 0)
                return PartialView("_IvaAddOrEdit", new Iva());
            else
                return PartialView("_IvaAddOrEdit", context.Iva.Find(id));
        }

        // POST: TypeOfVisitor/Create
        // POST: TypeOfVisitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IvaAddOrEdit(int id, Iva iva)
        {
            if (ModelState.IsValid)
            {
                if (iva.Ivaid == 0)
                {
                    var ivaExist = context.Iva
                        .Where(x => x.Description.ToLower().Contains(iva.Description.ToLower()) || x.Rate == iva.Rate).ToList();
                    if (ivaExist.Count != 0)
                    {
                        return Json(new
                        {
                            isValid = false,
                            html = Utils.RenderRazorViewToString(this, "Index", iva)
                        });
                    }
                    context.Add(iva);
                }
                else
                {
                    context.Update(iva);
                }
                await context.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Utils.RenderRazorViewToString(this, "Index", context.Iva.ToListAsync())
                });
            }
            return View(iva);
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> IvaDelete(int id)
        {
            var status = false;
            var ivaDelete = await context.Iva.FindAsync(id);
            var dt = await context.Product.Where(x => x.Ivaid == id).FirstOrDefaultAsync();
            if (dt != null)
            {
                status = false;
                return Json(status);
            }

            if (ivaDelete != null)
            {
                context.Iva.Remove(ivaDelete);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
    }
}
