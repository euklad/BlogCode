using BlazorForms;
using BlazorFormsSeed.Flows;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// BlazorForms
builder.Services.AddServerSideBlazorForms();
builder.Services.AddBlazorFormsMudBlazorUI();
builder.Services.AddBlazorFormsServerModelAssemblyTypes(typeof(SampleFlow));

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

