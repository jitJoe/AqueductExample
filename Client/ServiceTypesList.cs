using System;
using System.Collections.Generic;
using System.Reflection;
using Aqueduct.Shared.Proxy;
using AqueductExample.Shared;

namespace AqueductExample.Client
{
    public class ServiceTypesList : ITypeList
    {
        public List<Type> GetAllowedTypes()
        {
            return new List<Type>();
        }

        public List<Assembly> GetAllowedAssemblies()
        {
            return new List<Assembly>
            {
                //This assembly for service implementations + local service interfaces
                GetType().Assembly,
                //Shared assembly for service interfaces
                typeof(SerialisableTypesList).Assembly
            };
        }
    }
}