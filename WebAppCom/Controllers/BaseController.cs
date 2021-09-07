using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAppCom.Models;

namespace WebAppCom.Controllers
{
    public class BaseController : Controller
    {
        private readonly AplicacionComercialContext _context;

        public BaseController(AplicacionComercialContext context)
        {
            _context = context;
        }

        //GET : Departamentos locales
        public List<Department> Getdepartment()
        {

            List<Department> departments = new List<Department>();

            //get data
            departments = (from x in _context.Department
                           select x).ToList();
            //Inserting select item in List
            departments.Insert(0, new Department { DepartmentId = 0, Description = "Seleccionar" });

            return departments;
        }

        //GET : Bodegas
        public List<Bodega> GetBodegas()
        {
            List<Bodega> bodegasList = new List<Bodega>();

            //get data
            bodegasList = (from x in _context.Bodega
                           select x).ToList();
            //Inserting select item in List
            bodegasList.Insert(0, new Bodega { BodegaId = 0, Description = "Seleccionar" });

            return bodegasList;
        }

        //GET : Productos
        public List<Product> GetProducts()
        {
            List<Product> ProductsList = new List<Product>();

            //get data
            ProductsList = (from x in _context.Product
                           select x).ToList();
            //Inserting select item in List
            ProductsList.Insert(0, new Product { ProductId = 0, Description = "Seleccionar" });

            return ProductsList;
        }
    }
}
