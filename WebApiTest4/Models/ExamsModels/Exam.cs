using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiTest4.Models.ExamsModels
{
    public class Exam
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual IEnumerable<User> CurrentUsers { get; set; }
        public virtual IEnumerable<Train> Trains { get; set; }
        public virtual IEnumerable<TaskTopic> TaskTopics { get; set; }
    }

    public class EgeExam : Exam
    {
        
    }

    public class OgeExam : Exam
    {

    }
}