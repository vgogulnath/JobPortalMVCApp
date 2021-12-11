using JobPortalMVCApplication.BusinessLogic.Service;
using JobPortalMVCApplication.BusinessLogic.Interface;
using JobPortalMVCApplication.Models;
using System.Configuration;
using JobPortalMVCApplication.Filter;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc(options => {
    options.Filters.Add(new SessionFilter());
    options.Filters.Add(typeof(SessionFilter));
});
builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<IJobsLogic, JobsLogic>();
builder.Services.AddSingleton<ICommonLogic, CommonLogic>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
