using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest4.Models.ExamsModels
{
    
    
    public class TaskTopic{
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [Index(IsUnique = true)]
        public int Number { get; set; }
        [DefaultValue(0)]
        public int PointsPerTask { get; set; }
        [Required]
        [DefaultValue(false)]
        public bool IsShort { get; set; }
        [DefaultValue("")]
        public string Code { get; set; }

        [Required]
        public virtual Exam Exam { get; set; }

        public virtual List<ExamTask> ExamTasks { get; set; }
        
    }
}