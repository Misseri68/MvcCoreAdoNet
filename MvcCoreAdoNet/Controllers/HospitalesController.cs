using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class HospitalesController : Controller
    {

        private RepositoryHospital repo;

        public HospitalesController()
        {
            this.repo = new RepositoryHospital();
        }


        public IActionResult Index()
        {
            List<Hospital> hospitales = this.repo.GetHospitales();
            return View(hospitales);
        }

        //El parámetro debe llamarse igual que como se ha enviado en el ActionLink
        public IActionResult Details(int idHospital)
        {
            Hospital hospital = this.repo.FindHospital(idHospital);
            return View(hospital);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hospital hospital) {
            this.repo.CreateHospital(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["MENSAJE"] = "Hospital insertado";
            return RedirectToAction("Index");
        }

        public IActionResult Update(int idHospital) { 
            Hospital hospital = this.repo.FindHospital(idHospital);
            return View(hospital);   
        }

        [HttpPost]
        public IActionResult Update(Hospital hospital)
        {
            this.repo.UpdateHospital(hospital.IdHospital, hospital.Nombre, hospital.Direccion, hospital.Telefono, hospital.Camas);
            ViewData["MENSAJE"] = "Hospital modificado";
            return View(hospital);
        }

        public IActionResult Delete(int idHospital) {
            this.repo.DeleteHospital(idHospital);
            ViewData["MENSAJE"] = "Hospital eliminado";
            return RedirectToAction("Index");
        }
        
    }
}
