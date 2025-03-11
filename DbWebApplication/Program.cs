using DbWebApplication.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices()
    .AddStandardServices()
    .AddAppCookie()
    .AddDbContext(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
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