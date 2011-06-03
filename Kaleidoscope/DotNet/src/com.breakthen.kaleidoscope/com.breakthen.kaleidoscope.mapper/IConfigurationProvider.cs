using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public interface IConfigurationProvider
    {
        IMappingRunner FindRunner(Type type);

        List<IMappingRunner> AllMappingRunners { get; }
    }
}
