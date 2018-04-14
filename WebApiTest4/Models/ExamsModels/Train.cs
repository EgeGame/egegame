using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WebApiTest4.Models.ExamsModels
{
    //[Table("Train")]
    public class Train
    {
        public Train()
        {
            StartTime = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        
        public DateTime? StartTime { get; set; }

        public DateTime? FinishTime { get; set; }

        [Required]
        public virtual User User { get; set; }
        public virtual Exam Exam { get; set; }

        public virtual List<UserTaskAttempt> TaskAttempts { get; set; } = new List<UserTaskAttempt>();
    }
    //[Table("ExamTrain")]
    public class ExamTrain : Train
    {
        [DefaultValue(0)]
        public int? Points { get; set; }
    }
    //[Table("FreeTrain")]
    public class FreeTrain : Train
    {
        
    }

    public static class TrainExtensions
    {
        public static IEnumerable<Train> TrainsOfUsersCurrentExamType(this IEnumerable<Train> trains)
        {
            return trains.ToList().Where(x => x.Exam.GetType() == x.User.CurrentExam.GetType());
        }
        public static IEnumerable<Train> OfExamType(this IEnumerable<Train> trains, Type examType)
        {
            return trains.ToList().Where(x => x.Exam.GetType() == examType);
        }
    }

}