using _360.DataAccess.Data;
using _360.Utility;
using _360.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        void IDbInitializer.Initialize()
        {
            //Check for pending Migrations and migrate if any
            try 
            {
                if(_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            // After Migrations have been completed check if the Roles Exist 
            //If roles do not exist create the roles and create a default Admin User
            //Administer the user to Admin Role
            if(!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();


                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "fikayo@yahoo.com",
                    Email = "fikayo@yahoo.com",
                    Name = "Fikayo Oluwakeye",
                    HomeAddress = "3118 Green Stone Road",
                    PhoneNumber = "4038696903",
                    State = "SK",
                    City = "Regina"
                },"Fik@yo123!").GetAwaiter().GetResult();

                ApplicationUser user = _db.applicationUsers.FirstOrDefault(x=>x.UserName == "fikayo@yahoo.com");
                if(user != null)
                {
                    _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
                }


            }
            return;
        }
    }
}
