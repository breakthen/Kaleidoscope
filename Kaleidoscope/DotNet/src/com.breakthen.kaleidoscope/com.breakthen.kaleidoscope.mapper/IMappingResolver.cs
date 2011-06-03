using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public interface IMappingResolver
    {
        ResolutionResult resolve(ResolutionContext context);
    }
}
