using Filminurk.Core.ServiceInterface;
using FilminurkTARpe24_Markus.ServiceInterface;
using Filminurk.Data;
using Filminurk.ApplicationServices.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFilesServices, FilesServices>();
builder.Services.AddScoped<IUserCommentsServices, UserCommentsServices>();
builder.Services.AddDbContext<FilminurkTARpe24Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString
("DefaultConnection")));
builder.Services.AddScoped<IMovieServices, MovieServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
