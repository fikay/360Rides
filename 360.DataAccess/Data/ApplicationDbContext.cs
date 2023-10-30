using _360.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options):base(options) 
        {
        }

       public  DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<ServicesModel> services { get; set; }
        public DbSet<ServiceRequest> serviceRequests { get; set; }
        
        public DbSet<ChildrenName> ChildrenNames { get; set; }

        public DbSet<ReceivedRequestHeader> receivedRequestHeaders { get; set; }

        public DbSet<ReceivedRequestDetails> ReceivedRequestDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ServicesModel>().HasData(new ServicesModel
            {
                Id = 1,
                ServiceName = "Child Pickup",
                ServiceDescription = "Schedule dates for pickup for your children and we will be there to pick them Up",
                UpdatedBy = "Fikayo",
                UpdatedDate = DateTime.Now,
                CreatedBy = "Fikayo"
            });



        }
    }
}
