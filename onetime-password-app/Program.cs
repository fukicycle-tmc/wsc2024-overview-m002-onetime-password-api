using onetime_password_app.Components;
using onetime_password_app.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents()
                .AddCircuitOptions(options =>
                {
                    options.DetailedErrors = true;
                    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromSeconds(1);
                });
builder.Services.AddControllers();
builder.Services.AddSingleton<UserListService>();
builder.Services.AddSingleton<OneTimePasswordService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();