using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace BrumORM
{
    public class DatabaseDataConverter
    {
        public List<T> ConvertDataTableToEntities<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = ConvertDataRowToEntity<T>(row);
                data.Add(item);
            }
            return data;
        }

        public T ConvertDataRowToEntity<T>(DataRow dr)
        {
            Type tempEmptyObjType = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                var props = tempEmptyObjType.GetProperties().Where(x => x.GetCustomAttribute<DatabaseColumnNameAttribute>() != null || x.Name == column.ColumnName);

                foreach (PropertyInfo pro in props)
                {
                    var attr = pro.GetCustomAttribute<DatabaseColumnNameAttribute>();
                    if (attr != null)
                    {
                        if (attr.DatabaseColumnName == column.ColumnName)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                    else
                    {
                        if (pro.Name == column.ColumnName)
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                }
            }
            return obj;
        }

    }
}
