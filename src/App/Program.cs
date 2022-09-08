using Altinn.App.Api.Controllers;
using Altinn.App.Api.Extensions;
using Altinn.App.Api.Helpers;
using Altinn.App.Core.Interface;
using Altinn.App.Generated.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

void RegisterCustomAppServices(IServiceCollection services, IConfiguration config)
{
    // Register your apps custom service implementations here.
    // TODO: Link to documentation
}

// ###########################################################################
// # Unless you are sure what you are doing do not change the following code #
// ###########################################################################

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

ConfigureWebHostBuilder(builder.WebHost);

WebApplication app = builder.Build();

Configure();

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    // Add API controllers from Altinn.App.Api
    IMvcBuilder mvcBuilder = services.AddControllersWithViews();
    mvcBuilder
        .AddApplicationPart(typeof(InstancesController).Assembly)
        .AddXmlSerializerFormatters()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        });

    // Register custom implementations for this application
    RegisterCustomAppServices(services, config);

    // Register services required to run this as an Altinn application
    services.AddAltinnAppServices(config, builder.Environment);

    // Altinn App Model implementation service (The concrete implementation of IAppModel for this app)
    services.AddTransient<IAppModel, AppModel>();

    // Add Swagger support (Swashbuckle)
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Altinn App Api", Version = "v1" });
        StartupHelper.IncludeXmlComments(c.IncludeXmlComments);
    });
}

void ConfigureWebHostBuilder(IWebHostBuilder builder)
{
    builder.ConfigureAppWebHost(args);
}

void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    string applicationId = StartupHelper.GetApplicationId();
    if (!string.IsNullOrEmpty(applicationId))
    {
        app.UseSwagger(o => o.RouteTemplate = applicationId + "/swagger/{documentName}/swagger.json");

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"/{applicationId}/swagger/v1/swagger.json", "Altinn App API");
            c.RoutePrefix = applicationId + "/swagger";
        });
    }

    app.UseDefaultSecurityHeaders();
    app.UseRouting();
    app.UseStaticFiles('/' + applicationId);
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    app.UseHealthChecks("/health");
}
