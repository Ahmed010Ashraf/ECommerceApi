using AutoMapper;
using Domain.Contracts;
using EcommerceApp;
using EcommerceApp.CustomExceptionMiddleware;
using EcommerceApp.factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistance;
using Presistance.Data;
using Presistance.Reposatories;
using ServiceImplementation;
using ServiceImplementation.profiles;
using ServicesAbstraction;
using Shared.ErrorsModels;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWepAppServices();


//infrustructure services registration

builder.Services.AddInfrustructure(builder.Configuration);




//application services registration

builder.Services.AddApplicationServices(builder.Configuration);


var app = builder.Build();
#region data base initializer 
app.DataBaseInitializer().Wait();
#endregion

//custom exception middleware handler 
app.UseMiddleware<CustomExceptionMiddlewareHandeller>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
