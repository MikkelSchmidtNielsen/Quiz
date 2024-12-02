using Quiz.Components.Models;
using Microsoft.Data.SqlClient;
using System.Text;
using static Quiz.Components.DataAccessLayer.QuizDbContext;

namespace Quiz.Components.DataAccessLayer.Repository
{
	public class QuestionRepository
	{
		private readonly QuizDbContext _db;

		public QuestionRepository(QuizDbContext db)
		{
			_db = db;
		}

		/// <summary>
		/// (C)RUD
		/// </summary>
		/// <param name="question"></param>
		public void CreateQuestion(Question question)
		{
			using var connection = _db.GetConnection();

			string query =
				"insert into Questions (QuestionText, Answer1Text, Answer2Text, Answer3Text, Answer4Text, Seconds, CorrectAnswer)" +
				"values (@QuestionText, @Answer1Text, @Answer2Text, @Answer3Text, @Answer4Text, @Seconds, @CorrectAnswer)";

			var command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@QuestionText", question.QuestionText);
			command.Parameters.AddWithValue("@Answer1Text", question.AnswerOne);
			command.Parameters.AddWithValue("@Answer2Text", question.AnswerTwo);
			command.Parameters.AddWithValue("@Answer3Text", question.AnswerThree);
			command.Parameters.AddWithValue("@Answer4Text", question.AnswerFour);
			command.Parameters.AddWithValue("@Seconds", question.Seconds);
			command.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);

			command.ExecuteNonQuery();

			Console.WriteLine($"Question '{question.QuestionText}' successfully added.");
		}

		/// <summary>
		/// C(R)UD
		/// </summary>
		/// <returns></returns>
		public List<Question> GetQuestion()
		{
			List<Question> listOfQuestions = new List<Question>();

			using var connection = _db.GetConnection();

			string query = "select * from Questions";

			var command = new SqlCommand(query, connection);

			SqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				Question question = new Question();
				question.QuestionID = Convert.ToInt32(reader["QuestionID"]);
				question.QuestionText = reader["QuestionText"].ToString();
				question.AnswerOne = reader["Answer1Text"].ToString();
				question.AnswerTwo = reader["Answer2Text"].ToString();
				question.AnswerThree = reader["Answer3Text"].ToString();
				question.AnswerFour = reader["Answer4Text"].ToString();
				question.Seconds = Convert.ToInt32(reader["Seconds"]);
				question.CorrectAnswer = Convert.ToInt32(reader["CorrectAnswer"]);
				question.SoundFile = reader["SoundFile"].ToString();
				listOfQuestions.Add(question);
			}

			return listOfQuestions;
		}

		public void DeleteQuestion(Question question)
		{
			using var connection = _db.GetConnection();

			string query = 
				"delete from Questions " +
				"where QuestionID = @QuestionID";

			var command = new SqlCommand(query, connection);

			command.Parameters.AddWithValue("@QuestionID", question.QuestionID);

			command.ExecuteNonQuery();
		}
	}
}
