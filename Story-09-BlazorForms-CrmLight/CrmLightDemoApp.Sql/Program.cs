using BlazorForms;
using BlazorForms.Flows.Definitions;
using BlazorForms.Platform;
using BlazorForms.Platform.Stubs;
using CrmLightDemoApp.Onion;
using CrmLightDemoApp.Onion.Services.Flow;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Register Onion dependencies
builder.Services.AddOnionDependencies();

// MudBlazor
builder.Services.AddMudServices();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopStart;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

// BlazorForms
builder.Services.AddServerSideBlazorForms();
builder.Services.AddBlazorFormsMudBlazorUI();
builder.Services.AddBlazorFormsServerModelAssemblyTypes(typeof(PersonEditFlow));

var app = builder.Build();

// BlazorForms
app.BlazorFormsRun();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

