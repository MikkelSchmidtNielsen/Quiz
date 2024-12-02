using System;
using System.Data.SqlClient;
using Quiz.Components.DataAccessLayer.Repository;
using Quiz.Components.Models;

namespace Quiz
{
	public class GlobalData
	{
		//Dette er det nummer i spørgerækkefølgen vi er nået til
		public static int QuestionNumber { get; set; }

		//Dette er listen af spørgsmål
		public static List<Question> Questions { get; set; }

		public static int Points { get; internal set; }

		public static DateTime Timer { get; set; }

		public static void AddQuestions()
		{
			//Variable der skal bruges midlertidigt til at oprette spørgsmål
			QuestionRepository questions = new QuestionRepository(new Components.DataAccessLayer.QuizDbContext());

			List<Question> listOfQuestions = new List<Question>(questions.GetQuestion());

			GlobalData.Questions = listOfQuestions;

			GlobalData.Timer = DateTime.Now;
		}
	}
}
