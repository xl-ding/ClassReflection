using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using Model;

namespace Manager
{
    public class ColumnManager
    {
        public List<ColumnData> GetColumns()
        {
            return TableColums.Query();
        }
    }
}
