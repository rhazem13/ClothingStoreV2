using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClothingStoreV2.Areas.Identity.Data;
using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using ClothingStoreV2.Repositories;
using ClothingStoreV2.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder
    .Configuration.
    GetConnectionString("ClothingStore_IdentityContextConnection") ?? throw new 
    InvalidOperationException("Connection string 'ClothingStore_IdentityContextConnection' not found.");

//builder.Services.AddDbContext<ClothingStore_IdentityContext>(options =>
//   options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ClothingStoreContext>(options=>
    options.UseSqlServer(connectionString),ServiceLifetime.Transient) ;
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<ClothingStore_IdentityContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultUI()
    .AddEntityFrameworkStores<ClothingStoreContext>().AddDefaultTokenProviders();
builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    options =>
    {

        options.LoginPath = "/Identity/Account/Login";
        options.LogoutPath = "/Identity/Account/Logout";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";

    });
///////////////////////////////////////////////////////
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IUserDatumRepository,UserDatumRepository>();
////////////////////////////////////////////////////////
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
var app = builder.Build();
//CreateRolesService.CreateRoles(app.Services).Wait();
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

