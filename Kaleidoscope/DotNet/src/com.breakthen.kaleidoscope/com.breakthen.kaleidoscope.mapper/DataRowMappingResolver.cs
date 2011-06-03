using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace com.breakthen.kaleidoscope.mapper
{
    public class DataRowMappingResolver : IMappingResolver
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
                    string columnName = attribute == null
                        ? targetProperty.Name
                        : attribute.Name;

                    // Create expression representing r.CellValue<property.PropertyType>(property.Name)
                    MethodCallExpression propertyValueExpr = Expression.Call(
                        typeof(DataRowExtensions).GetMethod("CellValue").MakeGenericMethod(targetProperty.PropertyType),
                        context.Parameter, Expression.Constant(columnName));

                    MemberBinding memberBinding = Expression.Bind(targetProperty, propertyValueExpr);
                    memberBindings.Add(memberBinding);
                }
            }
            result.MemberBindings = memberBindings;
            return result;
        }
    }
}
