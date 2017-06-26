using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using CPL.Core.Managers;
using CPL.Core.Services;
using CPL.Data;

namespace CPL.API.App_Start
{
    public class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(ContainerConfig).Assembly).InstancePerLifetimeScope();
            //This registers 'A'
            builder.RegisterAssemblyModules(typeof(ContainerConfig).Assembly);

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        //This is 'A'
        public class RegisterServices : Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                //Add Interfaces here

                //___ Repositories _____
                builder.RegisterType<CoinCryptoRepository>().As<ICoinCryptoRepository>();
                builder.RegisterType<PreLaunchNotyUserRepository>().As<IPreLaunchNotyUserRepository>();
                builder.RegisterType<AuthRepository>().As<IAuthRepository>();

                //____Services
                builder.RegisterType<ErrorMessageService>().As<IErrorMessageService>();
                builder.RegisterType<UserAuthService>().As<IUserAuthService>();
                

                //___ Managers _____
                builder.RegisterType<AuthManager>().As<IAuthManager>();

                base.Load(builder);
            }
        }
    }
}