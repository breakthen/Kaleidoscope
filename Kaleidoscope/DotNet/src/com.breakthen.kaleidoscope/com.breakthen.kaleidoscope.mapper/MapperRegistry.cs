using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace com.breakthen.kaleidoscope.mapper
{
    public static class MapperRegistry
    {
        public const string DEFAULT_REGISTRY_KEY = "default";

        public static IDictionary<string, IMappingRunner> Registry = new Dictionary<string, IMappingRunner>
        {
            {typeof(IDataReader).FullName , new DataReaderMappingRunner()},
            {typeof(DataRow).FullName, new DataRowMappingRunner()},
            {DEFAULT_REGISTRY_KEY, new PlainObjectMappingRunner()}
        };

        //public static IMappingRunner this[string typeName]
        //{
        //    get
        //    {
        //        return _registry[typeName];
        //    }
        //}
    }
}
