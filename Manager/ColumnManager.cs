using System.Collections.Generic;
using Dal;
using Model;

namespace Manager
{
    public class ColumnManager
    {
        /// <summary>
        /// 获取数据库表字段
        /// </summary>
        /// <returns>表字段数据</returns>
        public List<ColumnData> GetColumns()
        {
            return TableColums.Query();
        }
    }
}
