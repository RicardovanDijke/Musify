using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playlist_Service.Database;
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
                opts.UseMySql(Configuration["ConnectionString:PlaylistDBMySql"]);
                opts.EnableSensitiveDataLogging();
            });
            services.AddScoped<DatabaseContext>();

            services.AddScoped<IPlaylistService, PlaylistService>();
            services.AddScoped<IPlaylistRepository, PlaylistRepository>();
          
            //services.AddControllers().AddNewtonsoftJson(options =>
            //    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

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