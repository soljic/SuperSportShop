using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Core.Service.UserService;
using Core.Services.Interfaces;
using Domain.Interfaces.Repositories.User;
using Microsoft.Owin.Builder;
using Owin;
using Persistence.Repositories.User;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;
using Web.Utilities;
using Web.Utilities.Helpers;
using Web.Utilities.ImageServices;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Konfiguracija OWIN middleware-a
            ConfigureAuth(app: new AppBuilder());
            ConfigureDependencyInjection();
           
        }

        private void ConfigureAuth(IAppBuilder app)
        {
            IdentityConfig.RegisterIdentity(app);
        }
        private void ConfigureDependencyInjection()
        {
            // Stvaranje kontejnera za Dependency Injection
            var container = new UnityContainer();
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            container.RegisterInstance(mapper);

            // Registracija implementacija i sučelja
            container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString));
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IImageService, ImageService>();
            // Postavljanje Dependency Resolvera
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

    }
}