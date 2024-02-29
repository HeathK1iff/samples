using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddControllersWithViews();
builder.Services
    .AddScoped<IUserValidatorService, UserValidatorService>()
    .AddAuthorization(options =>
    {
        options.AddPolicy("AdministratorRole",
            policy => policy.RequireRole("AdministratorX"));
        options.AddPolicy("UserRole",
            policy => policy.RequireRole("SimpleUser"));
    })
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ClaimsIssuer = "test";
        options.LoginPath = "/xx";// Default: /Account/Login?ReturnUrl=%2F
        options.ReturnUrlParameter = "";
        options.Cookie.Name = "my_cookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.Events = new CookieAuthenticationEvents()
        {

            OnValidatePrincipal = async (context) => {
                await Task.Run<Task>(async () => {  
                    Claim? userName = context.Principal?.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);
            
                    if (userName is null)
                    {   
                        context.RejectPrincipal();
                        return;
                    }

                    var userValidatorService = context.HttpContext.RequestServices.GetRequiredService<IUserValidatorService>();

                    if (!userValidatorService.IsAcceptedUser(userName.Value)){
                        context.RejectPrincipal();  
                        return;
                    } 
                });
            }
        };
    });

var app = builder.Build();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", async (HttpContext context) => {

    var service = context.RequestServices.GetRequiredService<IAuthenticationService>();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "test@mail.ru"),
        new Claim(ClaimTypes.Role, "Administrator"),
        new Claim("newToken", "My-API-token")
    };

    var claimsIdentity = new ClaimsIdentity(
        claims, CookieAuthenticationDefaults.AuthenticationScheme);

    var authProperties = new AuthenticationProperties
    {
        RedirectUri = "/login2" // Redirect if success auth
    };

    await service.SignInAsync(context, CookieAuthenticationDefaults.AuthenticationScheme, 
        new ClaimsPrincipal(claimsIdentity), authProperties);
});

app.MapGet("/admin", async (HttpContext context) => {
    await context.Response.WriteAsync("test2");
}).RequireAuthorization(f=>
    {
        f.RequireAuthenticatedUser();
        f.RequireRole("AdministratorX");
    });

app.Run();

public interface IUserValidatorService
{
    bool IsAcceptedUser(string userName);
}

public class UserValidatorService: IUserValidatorService
{
    private static User[] _users = new[] { new User(id: 1, name: "login1"), new User(id: 2, name: "test@mail.ru") };

    public User[] GetAll(){
        return _users;
    }

    public bool IsAcceptedUser(string name)
    {
        return GetAll().Any(user=>user.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}

public class User
{
    public User(int id, string name){
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }
}
