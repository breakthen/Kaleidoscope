using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.breakthen.kaleidoscope.mapper
{
    public static class ObjectExtensions
    {

        public static bool IsNotNull(this object value)
        {
            return !value.IsNull();
        }

        public static bool IsNull(this object value)
        {
            return (value == null);
        }

        public static T UnsafeCast<T>(this object value)
        {
            if (!value.IsNull())
            {
                return (T)value;
            }
            return default(T);
        }

        public static T SafeCast<T>(this object value)
        {
            if (value is T)
            {
                return UnsafeCast<T>(value);
            }
            return default(T);
        }
    }
}
