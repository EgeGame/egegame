using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class AttemptViewModel : ExamTaskViewModel
    {
        public AttemptViewModel(UserManualCheckingTaskAttempt attempt) : base(attempt.ExamTask)
        {
            attempt_id = attempt.Id;
            right_answer = attempt.ExamTask.Answer;
            student_id = attempt.Train.User.Id;
            student_name = attempt.Train.User.Name;
            student_answer = new StudentAnswer()
            {
                text = attempt.UserAnswer,
                comment = attempt.Comment,
                images = attempt.ImagesLinks
            };
        }
        public int attempt_id { get; set; }
        public int student_id { get; set; }
        public string student_name { get; set; }
        public string right_answer { get; private set; }
        public StudentAnswer student_answer { get; set; }
    }
    public class StudentAnswer
    {
        public string text { get; set; }
        public string comment { get; set; }
        public List<string> images { get; set; }
    }
}