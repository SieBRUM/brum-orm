using System;
using System.Collections.Generic;
using System.Text;

namespace BrumORM
{
    public class DatabaseColumnNameAttribute : Attribute
    {
        public string DatabaseColumnName;
        public DatabaseColumnNameAttribute(string databaseColumnName)
        {
            DatabaseColumnName = databaseColumnName;
        }
    }
}
