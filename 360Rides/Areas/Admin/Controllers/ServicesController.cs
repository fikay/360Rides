using _360.DataAccess.Data;
using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Models.DTO;
using _360.Utility;
using AutoMapper;
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
        private readonly IMapper _mapper;
        [BindProperty]
        private ServicesModel servicesModel {  get; set; }
      
        private ServicesAdminDTO adminDTO { get; set; }


        public ServicesController(ApplicationDbContext db, IUnitofWorkRepository unitOfWork, IMapper mapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public IActionResult Index()
        {
            ViewData["Title"] = "SERVICES";
            return View();
        }

        public IActionResult upsert( int id )
        {
            if(id == 0 )
            {
                ViewData["Title"] = "CREATE NEW  SERVICE";
               adminDTO = new ServicesAdminDTO();
            }
            else
            {

                ViewData["Title"] = "EDIT SERVICE";
                servicesModel = _unitOfWork.ServicesRepository.GetAsync(x => x.Id == id).GetAwaiter().GetResult();
                adminDTO = _mapper.Map<ServicesAdminDTO>(servicesModel);

            }
            return View(adminDTO);
        
        
        }

        [HttpPost]
        public IActionResult upsert(ServicesAdminDTO servicesModel)
        {
            //Find User from Users Database
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = _db.Users.FirstOrDefault(x => x.Id == userId) as ApplicationUser;
            var service = _mapper.Map<ServicesModel>(servicesModel);
            //If  ServiceModel Id is zero : NEW SERVICE BEING CREATED UPDATE CREATEDBY AND CREATEDDATE
            //If  ServiceModel Id is not zero : SERVICE IS BEING UPDATED. UPDATE UPDATEDBY AND UPDATEDDATE
            if (service.Id == 0)
            {         
                service.CreatedBy = user.Name;
                service.CreatedDate = DateTime.Now;
            }
            else
            {   
                service.UpdatedBy = user.Name;
                service.UpdatedDate = DateTime.Now;
            }
            
            if (TryValidateModel(service))
            {
                if(service.Id == 0)
                {
                    _unitOfWork.ServicesRepository.AddAsync(service);
                    TempData["Success"] = "New Service has been Created";
                }
                else
                {
                    _unitOfWork.ServicesRepository.update(service);
                    TempData["Success"] = "Service has been Updated";
                }
                
                _unitOfWork.save();
            }
           

            return RedirectToAction(nameof(Index));
        }

      
        #region API Calls
        public IActionResult GetAll()
        {
            List<ServicesModel> services1 = _unitOfWork.ServicesRepository.GetAllAsync().GetAwaiter().GetResult().ToList();
            var services = _mapper.Map<IEnumerable<ServicesAdminDTO>>(services1);

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
                var service = _unitOfWork.ServicesRepository.GetAsync(x => x.Id == id).GetAwaiter().GetResult();
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
