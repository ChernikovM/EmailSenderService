using DataAccessLayer.AppDataContext;
using DataAccessLayer.Repositories.EFRepository;
using DataAccessLayer.Repositories.EFRepository.Interfaces;
using EmailSenderService.Configurations;
using EmailSenderService.Configurations.Interfaces;
using EmailSenderService.GraphQL;
using EmailSenderService.Services;
using EmailSenderService.Services.Interfaces;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailSenderService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<IErrorRepository, ErrorRepository>();

            var smtpConfig =
                Configuration.GetSection("SmtpConfiguration").Get<SmtpConfiguration>();
            services.AddSingleton<ISmtpConfiguration>(smtpConfig);

            services.AddScoped<IMailService, MailService>();

            services.AddAuthentication(opt => opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => opt.LoginPath = "/account/googlelogin")
                .AddGoogle(opt =>
                {
                    opt.ClientId = Configuration["GoogleAuthConfiguration:ClientId"];
                    opt.ClientSecret = Configuration["GoogleAuthConfiguration:ClientSecret"];
                });

            services.AddControllers();
            
            services
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints
                .MapGraphQL("/api/emailSender/graphQL")
                .RequireAuthorization()
                ;
            });

            app.UseGraphQLVoyager(new VoyagerOptions()
            {
                GraphQLEndPoint = "/api/emailSender/graphQL"
            }, path: "/api/emailSender/voyager");
        }
    }
}
