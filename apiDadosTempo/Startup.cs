using apiDadosTempo.Adapters;
using apiDadosTempo.Context;
using apiDadosTempo.Interfaces.Adapter;
using apiDadosTempo.Interfaces.Repositories;
using apiDadosTempo.Interfaces.UseCases;
using apiDadosTempo.Repositories;
using apiDadosTempo.UseCase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace apiDadosTempo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var teste = Configuration.GetConnectionString("Database");
            // services.AddDbContext<ContextData>(options => options.UseMySql(Configuration.GetConnectionString("Database")));

            services.AddDbContext<ContextData>(options => options.UseSqlServer(Configuration.GetConnectionString("Database")));

            #region Implementação de Interfaces de Cidade Temperatura

            services.AddScoped<IRepositorioCidadeTemperatura, RepositorioCidadeTemperatura>();

            services.AddScoped<IRetornaBuscaCidadeResponseAdapter, RetornaBuscaCidadeResponseAdapter>();

            services.AddScoped<IUseCaseCidade, UseCaseCidade>();

            #endregion



            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Busca Climática por Cidade", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Busca Climática por Cidade v1"));
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
}
