using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Playlist_Service.Database;
using Playlist_Service.Message;
using Playlist_Service.Service;

namespace Playlist_Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            // services.AddDbContext<SongContext>(opts => opts.UseNpgsql(Configuration["ConnectionString:SongDB"]));
            services.AddDbContext<DatabaseContext>(opts =>
            {
                // opts.UseNpgsql(Configuration["ConnectionString:SongDB"]);
                //opts.UseLazyLoadingProxies().UseMySql(Configuration["ConnectionString:PlaylistDBMySql"]);
                //opts.UseLazyLoadingProxies().UseMySql(Configuration["ConnectionString:PlaylistDBK8s"], opts => opts.EnableRetryOnFailure());
                opts.UseLazyLoadingProxies().UseMySql(Configuration["ConnectionString:PlaylistDBDocker"], opts => opts.EnableRetryOnFailure());
                opts.EnableDetailedErrors();

            opts.EnableSensitiveDataLogging();

            }, ServiceLifetime.Singleton);

            services.AddTransient<DatabaseContext>();

            services.AddTransient<IPlaylistService, PlaylistService>();
            services.AddTransient<IPlaylistRepository, PlaylistRepository>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            //setup Messaging receiver
            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));
            services.AddHostedService<UserUpdateReceiver>();

            //configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Playlist API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapControllers();
            });
        }
    }
}
