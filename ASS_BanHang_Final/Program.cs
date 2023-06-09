using ASS_BanHang_Final.IServices;
using ASS_BanHang_Final.Models;
using ASS_BanHang_Final.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ASS_BanHang_FinalContextConnection") ?? throw new InvalidOperationException("Connection string 'ASS_BanHang_FinalContextConnection' not found.");

// Add Service to the DI Container

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IBillService, BillService>();
builder.Services.AddTransient<IBillDetailService, BillDetailService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
// Dang ky Interface IProductService se duoc implement boi ProductService
// Sau nay neu can bao tri code, co the thay doi de dang ma khong lam anh huong den cac phan code con lai.
// DI Container se tu dong tao va Inject cac Instance tuong ung vao cac doi tuong su dung.

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

app.UseSession();


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

// Nhay ve trang chu
app.UseStatusCodePagesWithReExecute("/Home/Index");

//app.Use((context, next) =>
//{
//    var path = context.Request.Path;

//    if (!path.Equals("/login", System.StringComparison.OrdinalIgnoreCase) && !context.Session.TryGetValue("username", out _))
//    {
//        context.Response.Redirect("/Account/Login");
//        return Task.CompletedTask;
//    }
//    return next();
//});

app.UseEndpoints(endpoints =>
{
    // Mapcontroller
    // MapControllerRoute
    // MapDefaultControllerRoute
    // MapAreaControllerRoute
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    
});



app.Run();
