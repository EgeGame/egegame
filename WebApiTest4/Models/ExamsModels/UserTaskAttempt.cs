using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace WebApiTest4.Models.ExamsModels
{
    public class UserTaskAttempt
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserAnswer { get; set; }

        [Required]
        [DefaultValue(0)]
        public int Points { get; set; }

        [Required]
        public virtual ExamTask ExamTask { get; set; }

        [Required]
        public virtual Train Train { get; set; }
    }

    public class UserSimpleTaskAttempt : UserTaskAttempt
    {
    }

    public class UserManualCheckingTaskAttempt : UserTaskAttempt
    {
        public DateTime? CheckTime { get; set; }

        [DefaultValue(false)]
        public bool IsChecked { get; set; }

        public virtual User Reviewer { get; set; }

        public string Comment { get; set; }
        //dirty hack to avoid another instance (imageLink) creation
        [NotMapped] private List<string> _imagesLinks;
        [NotMapped]
        public List<string> ImagesLinks
        {
            get { return _imagesLinks; }
            set { _imagesLinks = value; }
        }
        [DefaultValue("")]
        public string LinksAsString
        {
            get { return _imagesLinks != null ? string.Join(" ", _imagesLinks) : ""; }
            set { _imagesLinks = value?.Split(' ').ToList() ?? new List<string>(); }
        }
    }
}