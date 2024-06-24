using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RepoMVC.Data;
using RepoMVC.Repo;
using RepoMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(option=>{
    option.IdleTimeout=TimeSpan.FromMinutes(5); 
    option.Cookie.HttpOnly = true;
});

//Service for using Authentication and authozation
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    option.LoginPath = "/Auth/Signin";
    option.LogoutPath = "/Auth/Signin";
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(
       builder.Configuration.GetConnectionString("dbconn")

));
builder.Services.AddScoped<EmpRepo,EmpService>();
builder.Services.AddScoped<UserRepo,UserService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();   
app.UseRouting();
//for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Signin}/{id?}");

app.Run();
