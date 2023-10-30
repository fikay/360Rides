using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Models.ViewModels;
using _360.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _360Rides.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitofWorkRepository _unitofWork;
        [BindProperty]
        private RequestVM _requestVM { get; set; }

        public CartController(IUnitofWorkRepository unitofWork)
        {
            _unitofWork = unitofWork;
        }
        [Authorize(Roles = SD.Role_Customer + "," + SD.Role_Admin + "," + SD.Role_Employee)]
       
        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

           var cart =  _unitofWork.RequestRepository.GetAllAsync(x=>x.UserId == userId, includeProperties: "Service").GetAwaiter().GetResult();
            return View(cart);
        }
        [HttpPost, ActionName("Index")]
        public IActionResult sendRequest()
        {

            return View();
        }

        public IActionResult Details(int request)
        {
           
            ViewData["Title"] = "Edit Request";
            ServiceRequest requestFound = _unitofWork.RequestRepository.GetAsync(x => x.Id == request, includeProperties: "Service,childrenNames").GetAwaiter().GetResult();
            


            return View(requestFound);
        }
        [HttpPost]
        public IActionResult Details(ServiceRequest obj) 
        {
            obj.childrenNumber = obj.childrenNames.Count;
            foreach (var childName in obj.childrenNames)
            {
                childName.UserId = obj.UserId;
            }
            _unitofWork.RequestRepository.update(obj);

            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult removeChild(int id, int serviceId)
        {
            //var child  = _unitofWork.ChildrenRepository.GetAsync(x=>x.Id == id).GetAwaiter().GetResult();
            var request = _unitofWork.RequestRepository.GetAsync(x => x.Id == serviceId, includeProperties: "childrenNames").GetAwaiter().GetResult();
            var findChild = request.childrenNames.FirstOrDefault(x => x.Id == id);
            request.childrenNames.Remove(findChild);    
            request.childrenNumber =  request.childrenNumber - 1;
            _unitofWork.RequestRepository.update(request);
            _unitofWork.ChildrenRepository.Delete(findChild);


            _unitofWork.save();

            return RedirectToAction(nameof(Details), new { request = request.Id});

        }

        public IActionResult DeleteOrder(int request)
        {
            
            var requestService = _unitofWork.RequestRepository.GetAsync(x => x.Id == request, includeProperties: "childrenNames").GetAwaiter().GetResult();
            var user = requestService.UserId;
            foreach (var child in requestService.childrenNames)
            {
                _unitofWork.ChildrenRepository.Delete(child);
            }

            _unitofWork.RequestRepository.Delete(requestService);
            
            

            _unitofWork.save();
            HttpContext.Session.SetInt32(SD.SessionName, _unitofWork.RequestRepository.GetAllAsync(X => X.UserId == user).GetAwaiter().GetResult().Count());
            return RedirectToAction(nameof(Index));

        }
    }
}
