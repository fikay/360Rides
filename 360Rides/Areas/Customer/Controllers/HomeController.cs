
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _360Rides.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofWorkRepository _unitOfWork;
        [BindProperty]
        private ServicesModel servicesModel {  get; set; }

        public HomeController(ILogger<HomeController> logger, IUnitofWorkRepository unitofWorkRepository)
        {
            _logger = logger;
            _unitOfWork = unitofWorkRepository;
        }

        public IActionResult Index()
        {
            List<ServicesModel> listOfServices = _unitOfWork.ServicesRepository.GetAll().ToList();
            return View(listOfServices);
        }

        public IActionResult Details(int id)
        {
            return View();
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
    }
}
