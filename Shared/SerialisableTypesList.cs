using System;
using System.Collections.Generic;
using System.Reflection;
using Aqueduct.Shared.Proxy;

namespace AqueductExample.Shared
{
    public class SerialisableTypesList : ITypeList
    {
        public List<Type> GetAllowedTypes()
        {
            return new List<Type>
            {
                typeof(string),
                typeof(bool),
                typeof(int),
                typeof(decimal),
                typeof(char),
                typeof(Exception)
            };
        }

        public List<Assembly> GetAllowedAssemblies()
        {
            return new List<Assembly> { GetType().Assembly };
        }
    }
}