using _360.DataAccess.Data;
using _360.DataAccess.Migrations;
using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using ApplicationUser = _360.Models.ApplicationUser;

namespace _360Rides.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class ServicesController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IUnitofWorkRepository _unitOfWork;
        [BindProperty]
        private ServicesModel servicesModel {  get; set; }


        public ServicesController(ApplicationDbContext db, IUnitofWorkRepository unitOfWork)
        {
            _db = db;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult Index()
        {
            ViewData["Title"] = "Services";
            return View();
        }

        public IActionResult upsert( int id )
        {
            if(id == 0 )
            {
                ViewData["Title"] = "CREATE NEW  SERVICE";
                servicesModel = new ServicesModel();
            }
            else
            {
                ViewData["Title"] = "EDIT SERVICES";
                servicesModel = _unitOfWork.ServicesRepository.Get(x => x.Id == id);
            }
            return View(servicesModel);
        
        
        }

        [HttpPost]
        public IActionResult upsert(ServicesModel servicesModel)
        {
            //Find User from Users Database
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = _db.Users.FirstOrDefault(x => x.Id == userId) as ApplicationUser;

            //If  ServiceModel Id is zero : NEW SERVICE BEING CREATED UPDATE CREATEDBY AND CREATEDDATE
            //If  ServiceModel Id is not zero : SERVICE IS BEING UPDATED. UPDATE UPDATEDBY AND UPDATEDDATE
            if (servicesModel.Id == 0)
            {         
                servicesModel.CreatedBy = user.Name;
                servicesModel.CreatedDate = DateTime.Now;
            }
            else
            {   
                servicesModel.UpdatedBy = user.Name;
                servicesModel.UpdatedDate = DateTime.Now;
            }

            if (ModelState.IsValid)
            {
                if(servicesModel.Id == 0)
                {
                    _unitOfWork.ServicesRepository.Add(servicesModel);
                    TempData["Success"] = "New Service has been Created";
                }
                else
                {
                    _unitOfWork.ServicesRepository.update(servicesModel);
                    TempData["Success"] = "Service has been Updated";
                }
                
                _unitOfWork.save();
            }
            return RedirectToAction(nameof(Index));
        }

      
        #region API Calls
        public IActionResult GetAll()
        {
            List<ServicesModel> services = _unitOfWork.ServicesRepository.GetAll().ToList();
            return Json(new { services });
        }

        public IActionResult deleteservice(int id)
        {
            if (id == 0)
            {
                TempData["Error"] = "Service Not Found";
                return NotFound();

            }
            else
            {
                var service = _unitOfWork.ServicesRepository.Get(x => x.Id == id);
                if(service == null)
                {
                    return NotFound();
                }
                _unitOfWork.ServicesRepository.Delete(service);
                _unitOfWork.save();
            }

            return Json(new {success = true, message = "Delete Successful"});
        }

        #endregion

    }
}
