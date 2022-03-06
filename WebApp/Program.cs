using System;
using System.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Consumer;
using WebApp.Data;
using WebApp.Models;
using WebApp.Repositories.Implementations;
using WebApp.Repositories.Interfaces;
using WebApp.Seeder;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
;

builder.Services.AddDbContext<LibraryContext>(options =>
    // options.UseNpgsql(builder.Configuration.GetConnectionString("LibraryContextPostgres"))
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryContextMsSql"))
);
builder.Services.AddDbContext<AuthContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("AuthContextMsSql")); });
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Repositories
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IBookInstanceRepository, BookInstanceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ISeederRepository, DatabaseSeederRepository>();
builder.Services.Configure<RabbitMqConfiguration>(a => builder.Configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IConsumerService, ConsumerService>();
builder.Services.AddHostedService<ConsumerHostedService>();

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
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();