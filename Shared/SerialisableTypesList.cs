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
            return new()
            {
                typeof(string),
                typeof(bool),
                typeof(int),
                typeof(decimal),
                typeof(char),
                typeof(Exception),
                typeof(List<>).Assembly.GetType("System.Collections.ListDictionaryInternal")
            };
        }

        public List<Assembly> GetAllowedAssemblies()
        {
            return new() { GetType().Assembly };
        }
    }
}