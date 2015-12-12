using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;

using V8Commit.Services.ConversionServices;
using V8Commit.Services.HashServices;

namespace V8Commit.WebUI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            RegisterTypes(container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        public static void RegisterTypes(UnityContainer container)
        {
            container.RegisterType<IConversionService<UInt64, DateTime>, UInt64ToDateTime>(new ContainerControlledLifetimeManager());
            container.RegisterType<IHashService, MD5HashService>(new ContainerControlledLifetimeManager());
        }

    }
}