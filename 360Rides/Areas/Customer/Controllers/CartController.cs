using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _360Rides.Areas.Customer.Controllers
{
   
    public class CartController : Controller
    {
        private readonly IUnitofWorkRepository _unitofWork;

        public CartController(IUnitofWorkRepository unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [Authorize(Roles = SD.Role_Customer + "," + SD.Role_Admin + "," + SD.Role_Employee)]
        [Area("Customer")]
        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

           var cart =  _unitofWork.RequestRepository.GetAllAsync(x=>x.UserId == userId, includeProperties: "Service").GetAwaiter().GetResult();
            return View(cart);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
