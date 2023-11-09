using _360.DataAccess.Data;
using _360.DataAccess.Repository.Irepository;
using _360.Models;
using _360.Models.DTO;
using _360.Models.ViewModels;
using _360.Utility;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace _360Rides.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitofWorkRepository _unitofWorkRepository;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        [BindProperty]
        public EmployeeVM employeeVM { get; set; }

        public EmployeesController(ApplicationDbContext db, IUnitofWorkRepository unitofWorkRepository, IMapper _mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitofWorkRepository = unitofWorkRepository;
            mapper = _mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }
        public IActionResult Index()
        {
            
            ViewData["Title"] = "EMPLOYEES";
            return View();
        }


        public IActionResult upsert(Guid? id)
        {
            if (id != Guid.Empty || id  != null)
            {
                var employee = _unitofWorkRepository.EmployeesRepository.GetAsync(x => x.Id == id).GetAwaiter().GetResult();
                EmployeeDTO employeeDTO = mapper.Map<EmployeeDTO>(employee);
                employeeVM = new EmployeeVM()
                {
                    employeeDTO = employeeDTO,
                    roles = GetRoles()
                };

            }
            else
            {
                employeeVM = new EmployeeVM()
                {
                    employeeDTO = new EmployeeDTO(),
                    roles = GetRoles()
                };
            }
            

            return View(employeeVM);
        }

        [HttpPost, ActionName("upsert")]
        public IActionResult AddCreate(EmployeeVM employee)
        {
            if(ModelState.IsValid )
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                Guid guid = Guid.Parse(userId);
                if (employee.employeeDTO.Id == Guid.Empty)
                {
                    
                    Employees employees = mapper.Map<Employees>(employee.employeeDTO);
                    employees.CreatedById = guid;
                    employees.CreatedOn = DateTime.Now;
                    var defaultPassword = employees.Name.Replace(" ", "") + "@123!";
                    _userManager.CreateAsync(new ApplicationUser
                    {
                        UserName = employees.Email,
                        Name = employees.Name,
                        HomeAddress = employees.Address
                    }, defaultPassword).GetAwaiter().GetResult();
                    ApplicationUser user = _db.applicationUsers.FirstOrDefault(x => x.UserName == employees.Email);
                    if (user != null)
                    {
                        if(employees.Role == "Admin")
                        {
                            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                        }
                        else
                        {
                            _userManager.AddToRoleAsync(user, SD.Role_Employee).GetAwaiter().GetResult();
                        }
                        
                    }
                    _unitofWorkRepository.EmployeesRepository.AddAsync(employees).GetAwaiter().GetResult();
                    

                }
                else
                {
                    var prev = _unitofWorkRepository.EmployeesRepository.GetAsync(x => x.Id == employee.employeeDTO.Id).GetAwaiter().GetResult();

                    Employees employees = mapper.Map<Employees>(employee.employeeDTO);
                    employees.UpdatedById = guid;
                    employees.UpdatedOn = DateTime.Now;
                    employees.CreatedOn = prev.CreatedOn;
                    employees.CreatedById = prev.CreatedById;
                   
                    _unitofWorkRepository.EmployeesRepository.update(employees);
                }

                _unitofWorkRepository.save();
            }

            return RedirectToAction(nameof(Index));

        }

        public List<SelectListItem> GetRoles()
        {
            var roles = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>
            {
                 new SelectListItem { Text = SD.Employee_Driver, Value = SD.Employee_Driver },
                    new SelectListItem { Text = SD.Employee_Design, Value = SD.Employee_Design },
                    new SelectListItem { Text = SD.Employee_Transactions, Value = SD.Employee_Transactions},
                    new SelectListItem { Text = SD.Employee_Admin, Value = SD.Employee_Admin}

            };

            return roles;
        }
        #region
        public IActionResult GetAll()
        {
           var employees = _unitofWorkRepository.EmployeesRepository.GetAllAsync().GetAwaiter().GetResult();   

            return Json(new { employees });
        }
        #endregion
    }
}
