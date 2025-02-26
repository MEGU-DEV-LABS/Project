using DbWebApplication;
using DbWebApplication.Data;
using DbWebApplication.Extensions;
using DbWebApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ZXing.QrCode;
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices()
    .AddStandardServices()
    .AddAppCookie()
    .AddDbContext(builder.Configuration)
    .AddIdentity();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();