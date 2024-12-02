using Microsoft.Data.SqlClient;

namespace Quiz.Components.DataAccessLayer
{
	public class QuizDbContext
	{
		public string ConnectionString =
			"Data Source=localhost; " +
			"Initial Catalog=Question; " +
			"Integrated Security=SSPI; " +
			"TrustServerCertificate=true";

		public SqlConnection GetConnection()
		{
			var connection = new SqlConnection(ConnectionString);
			connection.Open(); //Open betyder at den tager connectionen og åbner den
			return connection;
		}
	}
}
