using DbWebApplication.Extensions;
using DbWebApplication.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCustomServices()
    .AddStandardServices()
    .AddAppCookie()
    .AddDbCustomContext(builder.Configuration)
    .AddRepositories()
    .AddAuthExtensions()
    .AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<UserIdMiddleware>();
app.UseAuthentication();   
app.UseAuthorization(); 
app.MapGet("/", context =>
{
    context.Response.Redirect("/Auth/Redirect");
    return Task.CompletedTask;
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}");
app.MapRazorPages();


app.Run();