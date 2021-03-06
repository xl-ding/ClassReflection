﻿using System.Collections.Generic;
using Model;
using Oracle.ManagedDataAccess.Client;

namespace Dal
{
    public class TableColums
    {
        public static List<ColumnData> Query()
        {
            string connStr = @"";
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                string sql = "select * from user_tab_columns where table_name='TTRD_CDS'";
                List<ColumnData> columnDatas = new List<ColumnData>();
                using (OracleCommand comm = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ColumnData column = new ColumnData(reader.GetString(1), reader.GetString(2));
                            columnDatas.Add(column);
                        }
                    }
                }

                return columnDatas;
            }
        }
    }
}
