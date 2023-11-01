using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;

using Microsoft.AspNetCore.Http;
using _360.Models;
using _360.Models.DTO;

using _360.Models;

using _360.Models.ViewModels;
using _360.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _360Rides.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitofWorkRepository _unitofWork;

        private readonly IMapper _mapper;

        [BindProperty]
        private RequestVM _requestVM { get; set; }

        public CartController(IUnitofWorkRepository unitofWork , IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        [Authorize(Roles = SD.Role_Customer + "," + SD.Role_Admin + "," + SD.Role_Employee)]
       
        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var userId = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

           var cart =  _unitofWork.RequestRepository.GetAllAsync(x=>x.UserId == userId, includeProperties: "Service").GetAwaiter().GetResult();
            var cartdto = _mapper.Map<IEnumerable<CartDTO>>(cart);
            ViewBag.itemCount = HttpContext.Session.GetInt32(SD.SessionName);
            
            return View(cartdto);
        }
        [HttpPost, ActionName("Index")]
        public IActionResult sendRequest()
        {

            return View();
        }


        public IActionResult Details(int requestId)
        {
           
            ViewData["Title"] = "Edit Request";
            ServiceRequest requestFound = _unitofWork.RequestRepository.GetAsync(x => x.Id == requestId, includeProperties: "Service,childrenNames").GetAwaiter().GetResult();
            var request = _mapper.Map<ServiceRequestDTO>(requestFound);
            request.Id = requestId;
            


            return View(request);
        }
        [HttpPost]
        public IActionResult Details(ServiceRequestDTO obj) 
        {
            obj.childrenNumber = obj.childrenName.Count;
            var serviceRequest = _mapper.Map<ServiceRequest>(obj);
            foreach (var childName in serviceRequest.childrenNames)
            {
                var child = _unitofWork.ChildrenRepository.GetAsync(x => x.Name == childName.Name).GetAwaiter().GetResult();
                if(child != null)
                {
                    childName.Id = child.Id;
                }
                childName.UserId = obj.UserId;
            }
            _unitofWork.RequestRepository.update(serviceRequest);


            _unitofWork.save();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult removeChild( int requestId, string Name)
        {
            
            //var child  = _unitofWork.ChildrenRepository.GetAsync(x=>x.Id == id).GetAwaiter().GetResult();
            var request = _unitofWork.RequestRepository.GetAsync(x => x.Id == requestId, includeProperties: "childrenNames").GetAwaiter().GetResult();
            var findChild = request.childrenNames.FirstOrDefault(x=>x.Name == Name);
            //var findChild = request.childrenNames.FirstOrDefault(x => x.Name == Name && x.UserId == request.UserId);
         
            request.childrenNames.Remove(findChild);
            request.childrenNumber = request.childrenNumber - 1;

            _unitofWork.RequestRepository.update(request);
            _unitofWork.ChildrenRepository.Delete(findChild);


            _unitofWork.save();

           

            return RedirectToAction(nameof(Details), new { requestId = request.Id});

        }

        public IActionResult DeleteOrder(int requestId)
        {
            
            var requestService = _unitofWork.RequestRepository.GetAsync(x => x.Id == requestId, includeProperties: "childrenNames").GetAwaiter().GetResult();

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
