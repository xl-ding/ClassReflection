namespace Model
{
    public class ColumnData
    {
        public ColumnData(string columnName,string columnType)
        {
            this.ColumnName = columnName;
            this.ColumnType = columnType;
        }

        /// <summary>
        /// 字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }
    }
}
