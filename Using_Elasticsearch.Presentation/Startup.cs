using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Using_Elasticsearch.Common.Configs;
using Using_Elasticsearch.Presentation.Common.Extensions;
using Using_Elasticsearch.DataAccess.DbInitializers;
using Using_Elasticsearch.Presentation.Middlewares;
using Using_Elasticsearch.DataAccess;

namespace Using_Elasticsearch.Presentation
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
            services.Configure<CorsConfig>(Configuration.GetSection(nameof(CorsConfig)));
            services.Configure<SwaggerConfig>(Configuration.GetSection(nameof(SwaggerConfig)));
            services.Configure<JwtConfig>(Configuration.GetSection(nameof(JwtConfig)));

            CorsExtension.AddCorsWithOrigin(services);
            SwaggerExtension.AddSwagger(services);

            Using_ElasticSearch.BusinessLogic.Configuration.Add(services, Configuration);

            JwtExtension.AddJwt(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlingMiddleware>();


            app.UseSwagger(Configuration);

            app.UseCors(Configuration.GetSection(nameof(CorsConfig)).GetSection(nameof(CorsConfig.PolicyName)).Value);           

            app.EnsureMigrate();

            initializer.Initialize().Wait();


            app.UseMvc();
        }
    }
}
