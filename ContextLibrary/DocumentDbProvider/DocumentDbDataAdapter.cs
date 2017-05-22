using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextLibrary.DocumentDbProvider
{
    [System.ComponentModel.DesignerCategory("")]
    public class DocumentDbDataAdapter : DbDataAdapter
    {
        protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
        {
            int results = 0;
            var resultTable = dataSet.Tables.Add();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var fields = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i));
                    foreach (var field in fields)
                    {
                        if (!resultTable.Columns.Contains(field))
                        {
                            var newColumn = resultTable.Columns.Add();
                            newColumn.ColumnName = field;
                            newColumn.AllowDBNull = true;
                        }
                    }
                    var newRow = resultTable.NewRow();

                    foreach (var field in fields)
                    {
                        newRow[field] = reader[field];
                    }
                    resultTable.Rows.Add(newRow);
                    results++;
                }
            }
            return results;
        }
    }
}
