using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public class DataRowMappingRunner : IMappingRunner
    {
        public ResolutionResult Map(ResolutionContext context)
        {
            ResolutionResult result = new ResolutionResult(context);
            result.MemberBindings.AddRange(this.AllResolvers[0].resolve(context).MemberBindings);
            return result;
        }

        public List<IMappingResolver> AllResolvers
        {
            get
            {
                return new List<IMappingResolver> { new DataRowMappingResolver() };
            }
        }
    }
}
