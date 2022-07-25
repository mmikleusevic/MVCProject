using System.Net;

var builder = WebApplication.CreateBuilder(args);

Uri endPoint = new Uri("https://localhost:7135/"); // this is the endpoint HttpClient will hit
HttpClient httpClient = new HttpClient()
{
    BaseAddress = endPoint,
};

httpClient.Timeout = TimeSpan.FromSeconds(60); // sixty seconds

builder.Services.AddSingleton<HttpClient>(httpClient);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
