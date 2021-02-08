using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstatePrice.Autofac
{
    public static class ModuleLoader
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static void LoadContainer(this ContainerBuilder builder, string pattern)
        {

            string executableLocation = Assembly.GetEntryAssembly().Location;
            IEnumerable<Assembly> assemblies = Directory
                .GetFiles(Path.GetDirectoryName(executableLocation), pattern, SearchOption.AllDirectories)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            builder.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();

            Type baseType = typeof(IModule);

            foreach (Assembly assembly in assemblies)
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope()
                    .EnableInterfaceInterceptors();
        }
    }
}