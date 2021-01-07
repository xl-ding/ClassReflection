using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ColumnData
    {
        public ColumnData(string columnName,string columnType)
        {
            this.ColumnName = columnName;
            this.ColumnType = columnType;
        }

        public string ColumnName { get; set; }

        public string ColumnType { get; set; }
    }
}
