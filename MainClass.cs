using System;
using System.Data;
using System.Data.SqlClient;
namespace BatchInsert
{
	public class MainClass
	{
		//create a datatable with the data you want to insert into database
		public static DataTable GetDataTableInsert(Model model,DataTable dataTable)
		{
			DataRow dr=dataTable.NewRow();
			dr["ID"]=model.ID;
			dr["Name"]=model.Name;
			dr["Age"]=model.Age;
			dataTable.Rows.Add(dr);
			return dataTable;
		}
		public static void DBatchInsert (DataTable dt)
		{
			string connectionString = "your connection string";
			SqlConnection conn = new SqlConnection (connectionString);
			SqlBulkCopy sbc = new SqlBulkCopy (conn);
			sbc.DestinationTableName = "your table name";
			sbc.BatchSize = dt.Rows.Count;
			try {
				conn.Open ();
				if (dt != null && dt.Rows.Count != 0) {
					sbc.WriteToServer (dt);
				}
			} catch (Exception ex) {
				throw ex;
			} finally {
				sbc.Close();
				conn.Close();
			}
		}
		public static void Main (string[] args)
		{
			DataTable dt = new DataTable ("BatchInsert");
			dt.Columns.Add ("ID");
			dt.Columns.Add ("Name");
			dt.Columns.Add ("Age");
			//assuming that every 1000 records at a time inserting
			for(int i=0;i<modelList.Count;i++)
			{
				for (int j=0; j<1000; j++) {
					GetDataTableInsert(model,dt);
				}
				DBatchInsert(dt);
			}
			Console.WriteLine("Dude,work done!");
		}
	}
}
