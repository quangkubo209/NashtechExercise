using ASPNet_Day1.BusinessLogic;
using ASPNet_Day1.Models.DTOs;
using ASPNet_Day1.Models.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddSingleton<IPersonRepository, PersonRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperPerson));

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

app.UseAuthorization();

app.MapControllers();


//app.MapControllerRoute(
//    name: "NashTech",
//    pattern: "{area:NashTech}/{controller=Person}/{action=ExportExcel}/{id?}"
//);


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "person",
    pattern: "{controller=Person}/{action=Index}/{id?}");

app.Run();
