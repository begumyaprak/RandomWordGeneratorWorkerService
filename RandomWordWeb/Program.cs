using Microsoft.EntityFrameworkCore;
using RandomWordService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ITextService,TextService>();

//builder.Services.AddScoped<Worker>();
builder.Services.AddDbContext<TextContext>(
    options => options.UseSqlServer("name=ConnectionStrings:DefaultConn")

    );


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TextList}/{action=Get}");

app.Run();
