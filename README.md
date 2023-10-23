# 360Rides - Website for 360 Business
A website to connect parents to ride services for transporting their kids to school and back to school for a fixed price. Looking at the confliting schedule of work and drop off toime for school, I realized i could ease the issue of having to wake up super early to drop off the kids by creating this service where parents set up a scheduled plan with 360Rides to always pick up their kids from home and drop them back at home at a specific time
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


## Integrating PNotify our alert system
```
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@Html.Raw("@")pnotify/core@5.2.0/dist/PNotify.min.css" />
<link href="https://cdn.jsdelivr.net/npm/@Html.Raw("@")pnotify/core/dist/BrightTheme.css" rel="stylesheet" />
<link href="https://cdn.jsdelivr.net/npm/pnotify@5.2.0/dist/PNotify.css" />
<link href="https://cdn.jsdelivr.net/npm/@Html.Raw("@")pnotify/confirm@5.2.0/dist/PNotifyConfirm.min.css" rel="stylesheet">


 <script src="https://cdn.jsdelivr.net/npm/@Html.Raw("@")pnotify/core@5.2.0/dist/PNotify.min.js"></script>
 <script src="https://cdn.jsdelivr.net/npm/@Html.Raw("@")pnotify/confirm@5.2.0/dist/PNotifyConfirm.min.js"></script>

``` 

[JsDelivr](https://www.jsdelivr.com/package/npm/@pnotify/confirm) was essential in providing the CDN for the PNotify  
The HTML.Raw was used to escape the @ character from being rendered as a server side variable.
