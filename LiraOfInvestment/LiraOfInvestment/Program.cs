using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Context>();

builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;



})
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();


builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login/Login";
    });

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.HttpOnly = true;
    config.ExpireTimeSpan = TimeSpan.FromMinutes(100);
    config.AccessDeniedPath = new PathString("/Login/AccessDenied");
    config.LoginPath = "/Login/Index/";
    config.SlidingExpiration = true;
});

builder.Services.AddTransient<ICompanyService, CompanyManager>();
builder.Services.AddTransient<ICompanyDal, EfCompanyDal>();

builder.Services.AddTransient<ICustomerService, CustomerManager>();
builder.Services.AddTransient<ICustomerDal, EfCustomerDal>();

builder.Services.AddTransient<IFavoriteService, FavoriteManager>();
builder.Services.AddTransient<IFavoriteDal, EfFavoriteDal>();

builder.Services.AddTransient<INewsService, NewsManager>();
builder.Services.AddTransient<INewsDal, EfNewsDal>();

builder.Services.AddTransient<IShareService, ShareManager>();
builder.Services.AddTransient<IShareDal, EfShareDal>();

builder.Services.AddTransient<ITeamService, TeamManager>();
builder.Services.AddTransient<ITeamDal, EfTeamDal>();

builder.Services.AddTransient<ITestimonialService, TestimonialManager>();
builder.Services.AddTransient<ITestimonialDal, EfTestimonialDal>();



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

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
});

app.Run();
