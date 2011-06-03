using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace com.breakthen.kaleidoscope.mapper
{
    public class DefaultConfigurationProvider : IConfigurationProvider
    {
        private static IConfigurationProvider provider;

        private static object sync = new object();

        public IMappingRunner FindRunner(Type type)
        {
            string key = type.FullName;
            if (MapperRegistry.Registry.ContainsKey(key))
            {
                return MapperRegistry.Registry[type.FullName];
            }
            return MapperRegistry.Registry[MapperRegistry.DEFAULT_REGISTRY_KEY];
        }

        public List<IMappingRunner> AllMappingRunners
        {
            get { throw new NotImplementedException(); }
        }

        public static IConfigurationProvider Current
        {
            get
            {
                if (provider == null)
                {
                    lock (sync)
                    {
                        if (provider == null)
                        {
                            provider = new DefaultConfigurationProvider(); 
                        }
                    }
                }
                return provider;
            }
        }
    }
}
