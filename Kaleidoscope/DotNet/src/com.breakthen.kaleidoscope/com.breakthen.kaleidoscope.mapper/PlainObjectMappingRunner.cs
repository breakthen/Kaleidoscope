using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public class PlainObjectMappingRunner: IMappingRunner
    {
        public ResolutionResult Map(ResolutionContext context)
        {
            ResolutionResult result = new ResolutionResult(context);
            foreach (var resolver in this.AllResolvers)
            {
                result.MemberBindings.AddRange(resolver.resolve(context).MemberBindings);
            }
            return result;
        }

        public List<IMappingResolver> AllResolvers
        {
            get 
            {
                return new List<IMappingResolver> { new PropertyMappingResolver(), new FieldMappingResolver() };
            }
        }
    }
}
