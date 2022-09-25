// ------------------------------------------------------------
// <copyright file="AssemblyMappingProfile.cs" company="ElectroSonne">
// Copyright (c) ElectroSonne, Russia, 2022.
// </copyright>
// ------------------------------------------------------------

using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Chat.Application
{
    /// <summary>
    /// AssemblyMappingProfile.
    /// </summary>
    public class AssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyMappingProfile"/> class.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        public AssemblyMappingProfile(Assembly assembly)
        {
            this.ApplyMappingsFromAssembly(assembly);
        }

        /// <summary>
        /// ApplyMappingsFromAssembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                .Any(i => i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
