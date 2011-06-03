using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public class Mapper
    {
        private static IMappingEngine engine = new MappingEngine();

        public static IMappingEngine Engine
        {
            get
            {
                return engine;
            }
        }

        public static TTarget Map<TSource, TTarget>(TSource source)
        {
            return engine.Map<TSource, TTarget>(source);
        }

        public static IEnumerable<TTarget> Map<TSource, TTarget>(IEnumerable<TSource> sourceCollection)
        {
            if (sourceCollection != null && sourceCollection.Count() > 0)
            {
                foreach (TSource source in sourceCollection)
                {
                    yield return Mapper.Map<TSource, TTarget>(source);
                }
            }
        }

        public static TTarget Map<TSource, TTarget>(TSource source, Func<TSource, TTarget> mapper)
        {
            return mapper(source);
        }

        public static TTarget Map<TSource, TTarget>(TSource source, IMappingEngine engine)
        {
            return engine.Map<TSource, TTarget>(source);
        }

    }
}
