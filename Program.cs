using System;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.Data.SqlClient;

namespace BulkIsertProject
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable tbl = new DataTable();
            //descreva todas as colunas da tabela definindo tipo equivalente:
            //Obs: Case - sensitive
            tbl.Columns.Add(new DataColumn("Document", typeof(Int32)));

            for (int i = 0; i < 10000; i++)
            {
                //preencha o datatable
                DataRow dr = tbl.NewRow();
                dr["Document"] = i;

                tbl.Rows.Add(dr);
            }
            string connection = "Data Source=127.0.0.1;Initial Catalog=Escola;User Id=sa;Password=123Senh@$";
            SqlConnection con = new SqlConnection(connection);
            //create object of SqlBulkCopy which help to insert  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            //assign Destination table name  
            objbulk.DestinationTableName = "Persons";
            //mapeamento das colunas para o objeto bulk
            objbulk.ColumnMappings.Add("Document", "Document");

            con.Open();
            //insert bulk Records into DataBase.  
            objbulk.WriteToServer(tbl);
            con.Close();
        }
    }
}
