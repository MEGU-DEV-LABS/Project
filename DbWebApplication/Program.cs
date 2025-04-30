using DbWebApplication.Extensions;
using DbWebApplication.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices()
    .AddStandardServices()
    .AddAppCookie()
    .AddDbCustomContext(builder.Configuration)
    .AddRepositories()
    .AddAuthExtensions();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseMiddleware<UserIdMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();   
app.UseAuthorization();  
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();