using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

namespace com.breakthen.kaleidoscope.mapper
{
    public class DataRecordMappingResolver : IMappingResolver
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
                    string fieldName = attribute == null
                        ? targetProperty.Name
                        : attribute.Name;

                    // Create expression representing r.Field<property.PropertyType>(property.Name)
                    MethodCallExpression propertyValueExpr = Expression.Call(
                        typeof(DataRecordExtensions).GetMethod("Field").MakeGenericMethod(targetProperty.PropertyType),
                        context.Parameter, Expression.Constant(fieldName));

                    MemberBinding memberBinding = Expression.Bind(targetProperty, propertyValueExpr);
                    memberBindings.Add(memberBinding);
                }
            }
            result.MemberBindings = memberBindings;
            return result;
        }
    }
}
