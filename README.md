In this CRUD is performed using MVC Repository and services and  Controller for Employee as well as Product.
Also it has how to upload Image in the Database in .NET MVC CORE using a file stream.

also it has authorization and authentication using inbuilt for .NET core using some function
Program.cs file it has all the services and middle ware 
basically we are protecting routes so
[Authorize] annotation on class will restrict that class or controller  from accessing routes
[allowAnonmymous] this will allow to access

//Service for using Authentication and authozation
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    option.LoginPath = "/Auth/SignIn";
    option.LogoutPath = "/Auth/SignIn";
});


//for authentication and authorization
app.UseAuthentication();
app.UseAuthorization();


    public IActionResult Signin(Login log)
    {
        var data = db.users.Where(x => x.Username.Equals(log.Username) && x.Password.Equals(log.Password)).SingleOrDefault();
        if (data != null)
        {
            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data.Username) }, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            HttpContext.Session.SetString("Username", data.Username);
            return RedirectToAction("Index", "Emp");
        }
        else
        {
            TempData["error"] = "Invalid Credentials!!";
            return RedirectToAction("Signin");
        }
    }

    public IActionResult SignOut()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var storedcookies = Request.Cookies.Keys;
        foreach (var cookie in storedcookies)
        {
            Response.Cookies.Delete(cookie);
        }
        return RedirectToAction("SignIn");
    }
}

   @if (User.Identity.IsAuthenticated)
   {
       <ul class="navbar-nav flex-grow-1">
           <li class="nav-item">
               <a class="nav-link text-dark" asp-area="" asp-controller="" asp-action="">Hello @User.Identity.Name</a>
           </li>
