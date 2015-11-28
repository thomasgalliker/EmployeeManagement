using System;
using System.Linq;

using Autofac;

using AutoMapper;

namespace Employee.Mapping.Configuration
{
    internal static class AutoMapperConfiguration
    {
        internal static void Configure(ContainerBuilder builder)
        {
            Mapper.Initialize(x => GetConfiguration(Mapper.Configuration, builder));
        }

        private static void GetConfiguration(IConfiguration configuration, ContainerBuilder builder)
        {
            var profileTypes = typeof(AutoMapperConfiguration).Assembly.GetTypes().Where(type => 
                    type.ContainsGenericParameters == false && 
                    typeof(Profile).IsAssignableFrom(type));

            foreach (var profileType in profileTypes)
            {
                var profile = Activator.CreateInstance(profileType) as Profile;
                configuration.AddProfile(profile);
                builder.RegisterInstance(profile).AsImplementedInterfaces().SingleInstance();
            }
        }
    }
}