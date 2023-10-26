
using _360.DataAccess.Data;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Models.ViewModels;
using _360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace _360Rides.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWorkRepository _unitOfWork;
        private readonly ApplicationDbContext _db;
        //[BindProperty]
        //private  RequestVM _requestVM {  get; set; }
        [BindProperty]
        private ServicesModel servicesModel {  get; set; }

        [BindProperty]
        private ServiceRequest serviceRequest { get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitofWorkRepository unitofWorkRepository, ApplicationDbContext db)
        {
            _logger = logger;
            _unitOfWork = unitofWorkRepository;
            _db = db;
        }

        public IActionResult Index()
        {

            ViewData["Title"] = "360 RIDES";
           

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            List<ServicesModel> listOfServices = _unitOfWork.ServicesRepository.GetAllAsync().GetAwaiter().GetResult().ToList();
            if(claim != null)
            {
                var inCart = _unitOfWork.RequestRepository.GetAllAsync(x => x.UserId == claim.Value).GetAwaiter().GetResult();
                HttpContext.Session.SetInt32(SD.SessionName, inCart.Count());
            }
            
            return View( listOfServices);

        }

        [Authorize(Roles = SD.Role_Customer + "," + SD.Role_Admin + "," + SD.Role_Employee)]
        public  IActionResult Details(int product)
        {
            // Get ASPUSER ID and ServiceChosen 
            // Find the User and set the service Id And User Id to the respective field in service request
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var serviceChosen = _unitOfWork.ServicesRepository.GetAsync( filter:x => x.Id == product).GetAwaiter().GetResult();
            var user = _db.Users.FirstOrDefault(x => x.Id == userId) as ApplicationUser;
            ServiceRequest request = new() {
                ServiceId = serviceChosen.Id,      
                UserId = user.Id,
            };
            // Check if the User has Kids in our DB and send list to the Viewbag
            var children = _unitOfWork.ChildrenRepository.GetAllAsync(x => x.UserId == userId).GetAwaiter().GetResult();
            if(children != null)
            {
                List<string> childNames = new List<string>();
                foreach (var child in children)
                {
                    childNames.Add(child.Name);
                    // Do something with the 'childName' variable, such as printing it or using it in your logic
                }
                ViewBag.Children = childNames;
            }
           
            ViewBag.Title = serviceChosen.ServiceName;

            return View(request);
        }
        [HttpPost]
        public  IActionResult Details(ServiceRequest request)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == request.UserId);
            foreach (var child in request.childrenNames)
            {
                    child.UserId = user.Id;
              //if (!_unitOfWork.ChildrenRepository.FindAsync(x => x.Name == child.Name ).GetAwaiter().GetResult())
              //  {
              //      _unitOfWork.ChildrenRepository.AddAsync(child).GetAwaiter().GetResult();
              //  }
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.ChildrenRepository.AddRangeAsync(request.childrenNames).GetAwaiter().GetResult();
                _unitOfWork.RequestRepository.AddAsync(request).GetAwaiter().GetResult();
                _unitOfWork.save();
                //_unitOfWork.save();
            }
            var servicerequest = _unitOfWork.RequestRepository.GetAllAsync(x => x.UserId == user.Id).GetAwaiter().GetResult();
            HttpContext.Session.SetInt32(SD.SessionName, servicerequest.Count());
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult getPrice(int distance)
        {
            double fee = 1.6;
            double flatRate = 4.00;
            double kilometers =  distance / (double) 1000; 
            // Round up to 1 decimal place (9.7 km)
            double roundedKilometers = Math.Round(kilometers * 10) / 10;
            var priceperday = (fee * roundedKilometers) + flatRate;
            priceperday = priceperday * 2;

            return Json(new { price = priceperday  });
        }
        #endregion
    }
}
