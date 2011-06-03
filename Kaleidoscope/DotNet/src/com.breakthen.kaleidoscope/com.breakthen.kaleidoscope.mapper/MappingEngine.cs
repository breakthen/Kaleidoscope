using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace com.breakthen.kaleidoscope.mapper
{
    public class MappingEngine : IMappingEngine
    {
        public TTarget Map<TSource, TTarget>(TSource source)
        {
            return MappingBuilder<TSource, TTarget>.Map(source);
        }
    }

    internal static class MappingBuilder<TSource, TTarget>
    {
        private static readonly Exception initializationException;
        private static readonly Func<TSource, TTarget> mapper;

        static MappingBuilder()
        {
            //MappingBuilder<TSource, TTarget>.mapper = BuildMapper();
            //MappingBuilder<TSource, TTarget>.initializationException = null;
            try
            {
                MappingBuilder<TSource, TTarget>.mapper = BuildMapper();
                MappingBuilder<TSource, TTarget>.initializationException = null;
            }
            catch (Exception e)
            {
                MappingBuilder<TSource, TTarget>.mapper = null;
                MappingBuilder<TSource, TTarget>.initializationException = e;
            }
        }

        private static Func<TSource, TTarget> BuildMapper()
        {
            Type sourceType = typeof(TSource);
            Type targetType = typeof(TTarget);
            ParameterExpression sourceParameter = Expression.Parameter(sourceType, "source");
            IMappingRunner runner = DefaultConfigurationProvider.Current.FindRunner(sourceType);
            ResolutionContext context = new ResolutionContext(sourceType, targetType, sourceParameter);
            ResolutionResult result = runner.Map(context);
            return Expression.Lambda<Func<TSource, TTarget>>(Expression.MemberInit(Expression.New(targetType), result.MemberBindings), new ParameterExpression[] { sourceParameter }).Compile();
        }

        internal static TTarget Map(TSource source)
        {
            if (initializationException != null)
            {
                throw initializationException;
            }
            if (source == null)
            {
                return default(TTarget);
            }
            return mapper(source);
        }
    }
}
