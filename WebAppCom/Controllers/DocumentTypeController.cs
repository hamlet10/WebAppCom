using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCom.Models;

namespace WebAppCom.Controllers
{
    public class DocumentTypeController : Controller
    {
        private readonly AplicacionComercialContext context;

        public DocumentTypeController(AplicacionComercialContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalClients = context.DocumentType.Count();
            return View();
        }


        #region Bodega

        public async Task<IActionResult> _DocumentTypeList()
        {
            #region ViewBags

            //ViewBag.fullName = (string)jObject.SelectToken("fullName");
            //ViewBag.email = (string)jObject.SelectToken("email");
            //ViewBag.ocupation = (string)jObject.SelectToken("occupation.name");
            //ViewBag.department = (string)jObject.SelectToken("department.name");
            //Conteo de las citas agendadas del dia

            #endregion

            return View(await context.DocumentType.ToListAsync());
        }

        // GET: TypeOfVisitor/partial/5
        // GET: TypeOfVisitor/Edit/5
        public IActionResult GetPartialDocumentType(int id = 0)
        {
            if (id == 0)
                return PartialView("_DocumentTypeAddOrEdit", new DocumentType());
            else
                return PartialView("_DocumentTypeAddOrEdit", context.DocumentType.Find(id));
        }

        // POST: TypeOfVisitor/Create
        // POST: TypeOfVisitor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DocumentTypeAddOrEdit(int id, DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                var dt = await context.DocumentType.Where(x => x.Description == documentType.Description && x.DocumentTypeId == 0).FirstOrDefaultAsync();
                if (documentType.DocumentTypeId == 0)
                {
                    if (dt != null)
                    {
                        ViewBag.dtMessage = "Este Tipo de Documento Ya existe";
                        return View(documentType);
                    }
                    context.Add(documentType);
                }
                else
                {
                    context.Update(documentType);
                }
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(_DocumentTypeList));
            }
            return View(documentType);
        }

        // GET: TypeOfVisitor/Delete/5
        public async Task<IActionResult> DocumentTypeDelete(int? id)
        {
            var status = false;
            var documentType = await context.DocumentType.FindAsync(id);
            var dt = context.Client.Where(x => x.DocumentTypeId == id).ToList();
            if (dt.Count > 0)
            {
                status = false;
            }

            if (documentType != null)
            {
                context.DocumentType.Remove(documentType);
                await context.SaveChangesAsync();
                status = true;
            }

            return Json(status);
        }
        #endregion
    }
}
