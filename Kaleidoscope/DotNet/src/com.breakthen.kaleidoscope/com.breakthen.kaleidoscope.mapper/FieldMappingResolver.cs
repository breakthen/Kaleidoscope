using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace com.breakthen.kaleidoscope.mapper
{
    public class FieldMappingResolver : IMappingResolver
    {
        public ResolutionResult resolve(ResolutionContext context)
        {
            ResolutionResult fieldResult = new ResolutionResult(context);
            List<MemberBinding> bindings = new List<MemberBinding>();
            foreach (FieldInfo targetField in from field in context.TargetType.GetFields(BindingFlags.Public | BindingFlags.Instance) select field)
            {
                MappingAttribute attribute = targetField.GetAttribute();
                if ((attribute == null) || !attribute.Ignored)
                {
                    MemberBinding memberBinding;
                    FieldInfo sourceField = context.SourceType.GetField(targetField.Name);
                    if (sourceField == null)
                    {
                        if (attribute != null)
                        {
                            sourceField = context.SourceType.GetField(attribute.Name);
                        }
                        if (sourceField == null)
                        {
                            continue;
                        }
                    }
                    if (!targetField.FieldType.IsAssignableFrom(sourceField.FieldType))
                    {
                        memberBinding = Expression.Bind(targetField, Expression.Convert(Expression.Field(context.Parameter, sourceField), targetField.FieldType));
                    }
                    else
                    {
                        memberBinding = Expression.Bind(targetField, Expression.Field(context.Parameter, sourceField));
                    }
                    bindings.Add(memberBinding);
                }
            }
            fieldResult.MemberBindings = bindings;
            return fieldResult;

        }
    }
}
