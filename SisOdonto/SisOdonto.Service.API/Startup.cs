using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SisOdonto.Infra.CrossCutting.Identity.Data;
using SisOdonto.Infra.CrossCutting.Identity.Models;
using SisOdonto.Infra.CrossCutting.IoC;
using SisOdonto.Infra.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SisOdonto.Service.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("Account/login");
                    //o.AccessDeniedPath = new PathString("/home/access-denied");
                });
            //.AddFacebook(o =>
            //{
            //    o.AppId = Configuration["Authentication:Facebook:AppId"];
            //    o.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //})
            //.AddGoogle(googleOptions =>
            //{
            //    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
            //    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});

            services.Configure<RequestLocalizationOptions>(
                            options =>
                            {
                                var supportedCultures = new List<CultureInfo>
                                {
                                    //new CultureInfo("en-US"),
                                    //new CultureInfo("es"),
                                    new CultureInfo("pt-BR"),
                                };

                                options.DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR");
                                options.SupportedCultures = supportedCultures;
                                options.SupportedUICultures = supportedCultures;

                                // You can change which providers are configured to determine the culture for requests, or even add a custom
                                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                                // By default, the following built-in providers are configured:
                                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                                options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                                //options.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
                                //options.RequestCultureProviders.Add(new RequestProvider()); // Add your custom culture provider back to the list
                            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddAutoMapperSetup();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CanWriteAdminData", policy => policy.Requirements.Add(new ClaimRequirement("Admin", "Write")));
            //    options.AddPolicy("CanRemoveAdminData", policy => policy.Requirements.Add(new ClaimRequirement("Admin", "Remove")));
            //});

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));
            services.AddDistributedMemoryCache();
            services.AddSession(op =>
            {
                op.IdleTimeout = TimeSpan.FromMinutes(30);
                op.Cookie.HttpOnly = true;
                op.Cookie.IsEssential = true;
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }

    }
}
