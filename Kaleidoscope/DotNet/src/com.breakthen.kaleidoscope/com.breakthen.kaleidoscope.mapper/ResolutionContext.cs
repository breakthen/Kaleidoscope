using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace com.breakthen.kaleidoscope.mapper
{
    public class ResolutionContext
    {
        private ParameterExpression parameter;
        private Type sourceType;
        private Type targetType;

        public ResolutionContext(Type sourceType, Type targetType, ParameterExpression parameter)
        {
            this.sourceType = sourceType;
            this.targetType = targetType;
            this.parameter = parameter;
        }

        public ParameterExpression Parameter
        {
            get
            {
                return this.parameter;
            }
        }

        public Type SourceType
        {
            get
            {
                return this.sourceType;
            }
        }

        public Type TargetType
        {
            get
            {
                return this.targetType;
            }
        }
    }
}
