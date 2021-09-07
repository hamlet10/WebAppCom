using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;

namespace WebAppCom.Controllers
{
    public class MaintenanceController : Controller
    {

        private readonly AplicacionComercialContext context;

        public MaintenanceController(AplicacionComercialContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalClient = context.Client.Count();
            return View();
        }


        #region Bodega

        public async Task<IActionResult> _BodegaList()
        {
            #region ViewBags

            //ViewBag.fullName = (string)jObject.SelectToken("fullName");
            //ViewBag.email = (string)jObject.SelectToken("email");
            //ViewBag.ocupation = (string)jObject.SelectToken("occupation.name");
            //ViewBag.department = (string)jObject.SelectToken("department.name");
            //Conteo de las citas agendadas del dia

            #endregion

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
                if (bodega.BodegaId == 0)
                    context.Add(bodega);
                else
                    context.Update(bodega);

                await context.SaveChangesAsync();
                return RedirectToAction(nameof(_BodegaList));
            }
            return View(bodega);
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> BodegaDelete(int? id)
        {
            var status = false;
            var Bodega = await context.Bodega.FindAsync(id);
            var bp = context.BodegaProduct.Where(x => x.BodegaId == id).ToList();
            if (bp.Count > 0)
            {
                status = false;
            }

            if (Bodega != null)
            {
                context.Bodega.Remove(Bodega);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
        #endregion
    }
}
