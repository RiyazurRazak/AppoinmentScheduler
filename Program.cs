using AppoinmentScheduler.Data;
using AppoinmentScheduler.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DB")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<Authentication>();
//app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseCors(MyAllowSpecificOrigins);

app.Run();
