using Microsoft.EntityFrameworkCore;
using Mission08_Team08.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TaskDBContext>(options =>
{
    options.UseSqlite(builder.Configuration["ConnectionStrings:TasksConnection"]);
});

// Register the EFTaskRepository as the implementation for TaskRepository
builder.Services.AddScoped<TaskRepository, EFTaskRepository>(); // This line is added

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
