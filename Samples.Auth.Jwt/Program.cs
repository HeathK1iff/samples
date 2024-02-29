using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

string jwtToken = string.Empty;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthorization();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "Issuer",
            ValidateAudience = true,
            ValidAudience = "services",
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretsecretsecretkey!123")),
            ValidateIssuerSigningKey = true
         };

        options.Events = new JwtBearerEvents(){
            OnTokenValidated = (context) => {
                Console.WriteLine("OnValidated");
                
                return Task.CompletedTask;
            },
            OnForbidden = async (context)=>{
                await context.HttpContext.ForbidAsync();
            }
        };

    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Hello World");

app.MapGet("/test", async (context) => { 
    Console.WriteLine("TEST");
    await context.Response.WriteAsync("test"); 
    
    }).RequireAuthorization();

app.MapGet("/login", (HttpContext context) => {
    Console.WriteLine("Creating token");
    var service = context.RequestServices.GetRequiredService<IAuthenticationService>();

    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, "test@mail.ru"),
        new Claim(ClaimTypes.Role, "Administrator"),
    };

    var jwt = new JwtSecurityToken(
            issuer: "Issuer",
            audience: "services",
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("mysupersecret_secretsecretsecretkey!123")), SecurityAlgorithms.HmacSha256));

    jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

    Console.WriteLine($"Assigned token: {jwtToken}");
});

app.MapGet("/call", () => {
    Console.WriteLine("Call");

    using (HttpClient client = new HttpClient()){
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwtToken) ;
        var response = client.GetAsync("http://localhost:5000/test").Result;

        Console.WriteLine($"IsSuccess: {response.IsSuccessStatusCode}");
    }    
});

app.Run();