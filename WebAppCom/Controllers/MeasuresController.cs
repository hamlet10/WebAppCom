using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;
using WebAppCom.Utility;

namespace WebAppCom.Controllers
{
    public class MeasureController : Controller
    {
        private AplicacionComercialContext context;

        public MeasureController(AplicacionComercialContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var measure = await (from x in context.Measure.Include(res => res.Product)
                          select x).ToListAsync();
            return View(measure);
        }

        //GET: Measure/Partial:5
        //GET: Measure/Edit:5
        public IActionResult GetPartialMeasure(int id)
        {
            if (id == 0)
                return PartialView("_MeasureAddOrEdit", new Measure());
            else
                return PartialView("_MeasureAddOrEdit", context.Measure.Find(id));
        }

        //POST: Measure
        public async Task<IActionResult> AddOrEdit(int id, Measure measure)
        {
            if (ModelState.IsValid)
            {
                if (measure.MeasureId == 0)
                {
                    var measureExist = context.Measure
                        .Where(x => x.Description.ToLower()
                        .Equals(measure.Description.ToLower()) || x.Abbreviation == measure.Abbreviation)
                        .ToList();
                    if (measureExist.Count != 0)
                    {
                        return Json(new
                        {
                            isValid = false,
                            html = Utils.RenderRazorViewToString(this, "Index", measure)
                        });
                    }
                    context.Add(measure);
                }
                else
                {
                    context.Update(measure);
                }
                await context.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Utils.RenderRazorViewToString(this, "Index", context.Measure.ToListAsync())
                });
            }
            return View(measure);
        }
        
        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> MeasureDelete(int id)
        {
            var status = false;
            var measureDelete = await context.Measure.FindAsync(id);
            //Valida si hay productos asociados.
            var dt = await context.Product.Where(x => x.MeasureId == id).FirstOrDefaultAsync();
            if (dt != null)
            {
                status = false;
                return Json(status);
            }

            if (measureDelete != null)
            {
                context.Measure.Remove(measureDelete);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
    }
}
