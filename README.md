# 360Rides - Website for 360 Business 
## Setting Up
- **Environments** - Visual Studio, SQL Server Management, GitHub.
- **.NET Version** - 8.0
- **Nuget Packages**

`
Microsoft.AspNetCore.Identity.EntityFrameworkCore - For Authentication
Microsoft.AspNetCore.Identity.UI - For Authentication e.g IEmail
Microsoft.EntityFrameworkCore - 
Microsoft.EntityFrameworkCore.Sqlite - Database connections
Microsoft.EntityFrameworkCore.SqlServer - Database connections
Microsoft.EntityFrameworkCore.Tools

`

## Connecting To Database SQL Server Management
In `appsettings.json`, I included the connectionstring:   
`
"ConnectionStrings": {
  "DefaultConnection": "Server=FAKS\\FIKAYOMYSQL;Database=360Rides;Trusted_Connection=true;TrustServerCertificate=True",
}
`

and in the Program.cs   
`
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
`

## Authentication

Download Nuget packages Above for authentication and scaffolded the Project using the identity and override all pages automatically created by ASP.NET.

Included in my ApplicationDbContext:  

`
 public class ApplicationDbContext : IdentityDbContext<IdentityUser>
 {
     public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options):base(options) 
     {
     }


     protected override void OnModelCreating(ModelBuilder builder)
     {
     //Very Important
         **base.OnModelCreating(builder);**
     }
 }
`  

In `Program.cs`, I included   
`
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
`

This is still throwing errors with the registration page in regards to the IEmailsender. Will have to create a Utility class for Iemail sender for it to use and see if that solves the issue.
