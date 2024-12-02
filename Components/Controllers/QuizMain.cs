using Microsoft.AspNetCore.Mvc;
using Quiz.Components.DataAccessLayer.Repository;
using Quiz.Components.Models;

namespace Quiz.Components.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuizMain : ControllerBase
	{
		QuestionRepository repository = new QuestionRepository(new DataAccessLayer.QuizDbContext());

		[HttpGet("{id}")]
		public ReturnPointAndQuestion Get(int id)
		{
			Console.WriteLine($"Recieved ID: {id}");

			//Move data from GlobalData to a return variable
			ReturnPointAndQuestion returnPointAndQuestion = new ReturnPointAndQuestion();

			if (id == 0)
			{
				GlobalData.QuestionNumber = 0;
			}
			else
			{
				//Check answer and give points
				int correctAnswer = GlobalData.Questions[GlobalData.QuestionNumber].CorrectAnswer;
				if (id == correctAnswer)
				{
					//Give points

					double elapsed = DateTime.Now.Subtract(GlobalData.Timer).TotalSeconds;


					if (elapsed < GlobalData.Questions[GlobalData.QuestionNumber].Seconds)
					{
						double points = 10000 / elapsed;

						GlobalData.Points += (int)points;
					}
				}

				//Add 1 to index
				GlobalData.QuestionNumber++;

			}

			if (GlobalData.QuestionNumber >= GlobalData.Questions.Count)
			{
				//No more questions
				returnPointAndQuestion.Question = new Question();
				returnPointAndQuestion.Question.QuestionText = "NO MORE"; //Message to the client so we know when to stop
				returnPointAndQuestion.Points = GlobalData.Points;
			}
			else
			{
				returnPointAndQuestion.Question = GlobalData.Questions[GlobalData.QuestionNumber];
				returnPointAndQuestion.Points = GlobalData.Points;

				if (GlobalData.Questions[GlobalData.QuestionNumber].Seconds == 0)
				{
					GlobalData.Questions[GlobalData.QuestionNumber].Seconds = 30; //default
																				  //returnPointAndQuestion.Question.seconds = 30; //Same as above
				}
			}

			GlobalData.Timer = DateTime.Now;

			return returnPointAndQuestion;

		}

		[HttpGet("GetQuestions")]
		public List<Question> GetQuestion()
		{
			List<Question> questions = GlobalData.Questions;
			return questions;
		}

		[HttpPost("SaveQuestion")]
		public void SaveQuestion(Question question) 
		{
			repository.CreateQuestion(question);
			GlobalData.Questions.Add(question);
		}

		[HttpPost("DeleteQuestion")]
		public void DeleteQuestion(List<Question> questions)
		{
			foreach (Question question in questions)
			{
				repository.DeleteQuestion(question);
				GlobalData.Questions.Remove(question);
			}
		}
	}
}
