using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.ExamsModels
{
    
    public class ExamTask
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public virtual TaskTopic TaskTopic { get; set; }
        

        public virtual List<UserTaskAttempt> UserTaskAttempts { get; set; }

    }
}