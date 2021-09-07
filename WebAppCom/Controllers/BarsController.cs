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
    public class BarsController : Controller
    {
        private readonly AplicacionComercialContext context;

        public BarsController(AplicacionComercialContext context)
        {
            this.context = context;
        } 

        //GET: Bar List
        public async Task<IActionResult> BarsList()
        {
            return View(await context.Bar.Include(x => x.Product).ToListAsync());
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> BarsDelete(int id)
        {
            var status = false;

            var deleteBar = await context.Bar.Where(x => x.ProductId == id).FirstOrDefaultAsync();
           
            if(deleteBar != null)
            {
                context.Bar.Remove(deleteBar);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
    }
}
