using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Song_Service.Database;
using Song_Service.Service;

namespace Song_Service
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
            // services.AddDbContext<SongContext>(opts => opts.UseNpgsql(Configuration["ConnectionString:SongDB"]));
            services.AddDbContext<DatabaseContext>(opts =>
            {
                // opts.UseNpgsql(Configuration["ConnectionString:SongDB"]);
                opts.UseLazyLoadingProxies().UseMySql(Configuration["ConnectionString:SongDBMySql"]);
                opts.EnableSensitiveDataLogging();
            });
            services.AddScoped<DatabaseContext>();

            // services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<SongService>();
            services.AddScoped<AlbumService>();
            services.AddScoped<IArtistRepository, ArtistRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();

            services.AddControllers().AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
