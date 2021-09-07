using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;

namespace WebAppCom.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AplicacionComercialContext context;

        public DepartmentController(AplicacionComercialContext context)
        {
            this.context = context;
        }

        //GET: Department List
        public async Task<IActionResult> _DepartmentList()
        {
            return View(await context.Department.ToListAsync());
        }

        // GET: Department/partial/5
        // GET: Department/Edit/5
        public IActionResult GetPartialDepartment(int id = 0)
        {
            if (id == 0)
                return PartialView("_DepartmentAddOrEdit", new Department());
            else
                return PartialView("_DepartmentAddOrEdit", context.Department.Find(id));
        }

        // POST: Department/Create
        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, Department department)
        {
            if (ModelState.IsValid)
            {
                var dt = await context.Department.Where(x => x.Description == department.Description && x.DepartmentId == 0).FirstOrDefaultAsync();
                if (department.DepartmentId == 0)
                {
                    if (dt != null)
                    {
                        ViewBag.dtMessage = "Este Departamento Ya existe";
                        return View(department);
                    }
                    context.Add(department);
                }
                else
                {
                    context.Update(department);
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(_DepartmentList));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> DepartmentDelete(int? id)
        {
            var status = false;
            var department = await context.Department.FindAsync(id);
            var dt = context.Product.Where(p => p.DepartmentId == id).FirstOrDefault();
            if (dt != null)
            {
                status = false;
            } 
            else if (department != null)
            {
                context.Department.Remove(department);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
    }
}
