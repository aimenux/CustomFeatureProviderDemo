﻿using Microsoft.FeatureManagement;
using WebApi.Features;

namespace WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddSingleton<IFeatureDefinitionProvider, InMemoryFeatureDefinitionProvider>()
            .AddFeatureManagement();

        services.AddScoped<IFeatureManagerSnapshot, InMemoryFeatureManagerSnapshot>();
        services.AddSingleton<IFeatureDefinitionProvider, InMemoryFeatureDefinitionProvider>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}