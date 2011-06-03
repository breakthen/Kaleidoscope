using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace com.breakthen.kaleidoscope.mapper
{
    public static class DataRecordExtensions
    {
        public static bool HasField(this IDataRecord record, string fieldName)
        {
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (record.GetName(i).Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static T Field<T>(this IDataRecord record, string fieldName)
        {
            T fieldValue = default(T);
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (string.Equals(record.GetName(i), fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    if (record[i] != DBNull.Value)
                    {
                        fieldValue = (T)record[fieldName];
                    }
                }
            }
            return fieldValue;
        }
    }
}
