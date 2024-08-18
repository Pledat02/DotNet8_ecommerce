using Ecommerce.Data;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using Ecommerce.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<MyDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Ecommerce"));
});

// Config session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

// Add application services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<HomeService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<Voucher_User>();
builder.Services.AddScoped<VoucherService>();
builder.Services.AddScoped<VnPayLibrary>();

builder.Services.AddHttpContextAccessor();

// Add utility configuration
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); // Ensure IHttpContextAccessor is available

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Apply migrations automatically
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MyDBContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        // Log error here if necessary
        Console.WriteLine("An error occurred while migrating the database.");
        Console.WriteLine(ex.Message);
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Configure JSON settings globally
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    Formatting = Newtonsoft.Json.Formatting.Indented,
    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
};
app.Run();
