using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace com.breakthen.kaleidoscope.mapper
{
    public static class DataRowExtensions
    {
        public static bool HasColumn(this DataTable dataTable, string columnName)
        {
            foreach (DataColumn column in dataTable.Columns)
            {
                if (column.ColumnName.Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static T CellValue<T>(this DataRow row, string columnName)
        {
            T value = default(T);
            if (row.Table.HasColumn(columnName))
            {
                value = (T)row[columnName];
            }
            return value;
        }
    }
}
