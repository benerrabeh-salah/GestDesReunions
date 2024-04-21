using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GestDesReunions.Models
{
	public class AdoNetHelper
	{
		private string connectionString;
		private SqlConnection connection;

		public AdoNetHelper(string connectionStringName)
		{
			this.connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
			this.connection = new SqlConnection(connectionString);
		}

		public SqlConnection Connection
		{
			get { return connection; }
		}

		public void OpenConnection()
		{
			try
			{
				if (connection.State == System.Data.ConnectionState.Closed)
				{
					connection.Open();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error opening connection: {ex.Message}");
			}
		}

		public void CloseConnection()
		{
			try
			{
				if (connection.State == System.Data.ConnectionState.Open)
				{
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error closing connection: {ex.Message}");
			}
		}

		public DataTable GetDataTable(string query)
		{
			DataTable dataTable = new DataTable();
			try
			{
				using (SqlCommand command = new SqlCommand(query, connection))
				{
					using (SqlDataAdapter adapter = new SqlDataAdapter(command))
					{
						adapter.Fill(dataTable);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error getting DataTable: {ex.Message}");
			}
			return dataTable;
		}
	}
}
