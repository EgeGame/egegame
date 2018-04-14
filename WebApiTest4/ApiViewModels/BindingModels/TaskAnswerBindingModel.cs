using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiTest4.ApiViewModels.BindingModels
{
    public class TaskAnswersSetBindingModel
    {
        [Required]
        public List<TaskAnswerBindingModel> list { get; set; }
        [Required]
        public string train_type { get; set; }

        
    }
    public class TaskAnswerBindingModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string answer { get;  set; }
        
        public string comment { get; set; }
        [MaxLength(5)]
        public string [] images { get; set; }
    }
}