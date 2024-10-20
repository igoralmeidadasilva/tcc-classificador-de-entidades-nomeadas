var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseMiddleware<PageNotFoundMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var seeder = app.Services.GetService<IDatabaseSeedService>();

await seeder!.ExecuteMigrationAsync();
await seeder!.ExecuteSeedAsync();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();