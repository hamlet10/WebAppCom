using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCom.Models;
using WebAppCom.Utility;

namespace WebAppCom.Controllers
{
    public class RoleController : Controller
    {
        private readonly AplicacionComercialContext context;

        public RoleController(AplicacionComercialContext _context)
        {
            context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var Roles = await (from x in context.Rol.Include(res => res.User)
                                 select x).ToListAsync();
            return View(Roles);
        }

        //GET: Measure/Partial:5
        //GET: Measure/Edit:5
        public IActionResult GetPartialRoles(int id)
        {
            if (id == 0)
                return PartialView("_RolesAddOrEdit", new Rol());
            else
                return PartialView("_RolesAddOrEdit", context.Rol.Find(id));
        }

        //POST: Measure
        public async Task<IActionResult> AddOrEdit(Rol rol)
        {
            if (ModelState.IsValid)
            {
                if (rol.RolId == 0)
                {
                    var rolExist = context.Rol
                        .Where(x => x.Description.ToLower().Equals(rol.Description.ToLower()))
                        .ToList();
                    if (rolExist.Count != 0)
                    {
                        return Json(new
                        {
                            isValid = false,
                            html = Utils.RenderRazorViewToString(this, "Index", rol)
                        });
                    }
                    context.Add(rol);
                }
                else
                {
                    context.Update(rol);
                }
                await context.SaveChangesAsync();
                return Json(new
                {
                    isValid = true,
                    html = Utils.RenderRazorViewToString(this, "Index", context.Rol.ToListAsync())
                });
            }
            return View(rol);
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> RolDelete(int id)
        {
            var status = false;
            var rolDelete = await context.Rol.FindAsync(id);
            //Valida si hay productos asociados.
            var dt = await context.User.Where(x => x.RolId == id).FirstOrDefaultAsync();
            if (dt != null)
            {
                status = false;
                return Json(status);
            }

            if (rolDelete != null)
            {
                context.Rol.Remove(rolDelete);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
    }
}
