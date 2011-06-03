using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace com.breakthen.kaleidoscope.mapper
{
    public static class MemberInfoExtensions
    {
        public static MappingAttribute GetAttribute(this MemberInfo member)
        {
            MappingAttribute[] attributes = member.GetCustomAttributes<MappingAttribute>();
            MappingAttribute attribute = null;
            if (attributes.Length != 0)
            {
                attribute = attributes[0];
            }
            return attribute;

        }

        public static T[] GetCustomAttributes<T>(this MemberInfo member) where T: Attribute
        {
            IEnumerable<T> queryResult = from attr in Attribute.GetCustomAttributes(member, typeof(T))
                                         select attr.SafeCast<T>();
            if (!queryResult.IsNull())
            {
                return queryResult.ToArray<T>();
            }
            return null;
        }
    }
}
