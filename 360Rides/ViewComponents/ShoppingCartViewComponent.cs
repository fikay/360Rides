using _360.DataAccess.Repository;
using _360.DataAccess.Repository.Irepository;
using _360.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _360Rides.ViewComponents
{
    public class ShoppingCartViewComponent: ViewComponent
    {

        public readonly IUnitofWorkRepository _UnitOfWork;

        public ShoppingCartViewComponent(IUnitofWorkRepository unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionName) == null)
                {
                    HttpContext.Session.SetInt32(SD.SessionName, _UnitOfWork.RequestRepository.GetAllAsync(X => X.UserId == claim.Value).GetAwaiter().GetResult().Count());
                    return View(HttpContext.Session.GetInt32(SD.SessionName));
                }

                return View(HttpContext.Session.GetInt32(SD.SessionName));
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }


        }
    }
}
