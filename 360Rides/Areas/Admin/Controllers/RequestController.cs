using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Models.DTO;
using _360.Models.ViewModels;
using _360.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _360Rides.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class RequestController : Controller
    {
        
        private readonly IUnitofWorkRepository _unitofWork;
        private readonly IMapper _mapper;

        public RequestController(IUnitofWorkRepository unitofWork, IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "REQUESTS";
            string status = HttpContext.Request.Query["status"];
            return View();
        }

        public IActionResult processing(int id)
        {
            ViewData["Title"] = "PROCESSING";
            var adminRequest = _unitofWork.ReceivedRequestHeader.GetAsync(x => x.Id == id, includeProperties: "user").GetAwaiter().GetResult();
            var requestDetails = _unitofWork.ReceivedRequestDetails.GetAsync(x => x.OrderheaderId == id, includeProperties: "Service,childrenNames").GetAwaiter().GetResult();
            AdminRequestDTO admin = _mapper.Map<AdminRequestDTO>(adminRequest);
            AdminRequestDetailsDTO details = _mapper.Map<AdminRequestDetailsDTO>(requestDetails);

            OrderVM orderVM = new OrderVM()
            {
                ReceivedRequestDetails = details,
                ReceivedRequestHeader = admin,
                selectListItems = new List<SelectListItem> {
                    new SelectListItem { Text = SD.Pending_status, Value = SD.Pending_status },
                    new SelectListItem { Text = SD.InProgress_status, Value = SD.InProgress_status },
                    new SelectListItem { Text = SD.Approved_status, Value = SD.Approved_status },
                    new SelectListItem { Text = SD.Declined_status, Value = SD.Declined_status }
                    }

            };


            return View(orderVM);
        }


        #region API CALLS
        public IActionResult GetAll()
        {
            string status = HttpContext.Request.Query["status"];
            var adminRequest = _unitofWork.ReceivedRequestHeader.GetAllAsync(includeProperties: "user").GetAwaiter().GetResult();
            List<AdminRequestDTO> admin = _mapper.Map<List<AdminRequestDTO>>(adminRequest);

            if (status != "All" && status != null)
            {
                admin = admin.Where(x=>x.orderStatus == status).ToList();
            }

            return Json(new { admin });
        }


        #endregion
    }

}
