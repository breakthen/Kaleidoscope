using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace com.breakthen.kaleidoscope.mapper
{
    public class PropertyMappingResolver : IMappingResolver
    {

        public ResolutionResult resolve(ResolutionContext context)
        {
            ResolutionResult result = new ResolutionResult(context);

            List<MemberBinding> memberBindings = new List<MemberBinding>();

            var targetProperties = from property in context.TargetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                   where property.CanWrite
                                   select property;

            foreach (PropertyInfo targetProperty in targetProperties)
            {
                MappingAttribute attribute = targetProperty.GetAttribute();
                if ((attribute == null) || !attribute.Ignored)
                {
                    PropertyInfo sourceProperty = context.SourceType.GetProperty(targetProperty.Name);
                    if (sourceProperty == null)
                    {
                        if (attribute != null)
                        {
                            sourceProperty = context.SourceType.GetProperty(attribute.Name);
                        }
                        if (sourceProperty == null)
                        {
                            continue;
                        }
                    }
                    if (sourceProperty.CanRead)
                    {
                        MemberBinding memberBinding;
                        if (!targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                        {
                            memberBinding = Expression.Bind(targetProperty, Expression.Convert(Expression.Property(context.Parameter, sourceProperty), targetProperty.PropertyType));
                        }
                        else
                        {
                            memberBinding = Expression.Bind(targetProperty, Expression.Property(context.Parameter, sourceProperty));
                        }

                        memberBindings.Add(memberBinding);
                    }
                }
            }
            result.MemberBindings = memberBindings;
            return result;
        }
    }
}
