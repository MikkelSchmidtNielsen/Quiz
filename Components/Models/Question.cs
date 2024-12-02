namespace Quiz.Components.Models
{
	public class Question
	{
        public int QuestionID { get; set; }

		public string QuestionText { get; set; }

        public string AnswerOne { get; set; }

		public string AnswerTwo { get; set; }

		public string AnswerThree { get; set; }

		public string AnswerFour { get;	set; }

		public int Seconds { get; set; }

		public int CorrectAnswer { get; set; }

		public string SoundFile { get; set; }

		public bool IsSelected {  get; set; }
	}
}
