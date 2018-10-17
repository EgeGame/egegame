using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Models
{
    public class TrainViewModel
    {
        public int id { get; set; }

        public DateTime? start_date { get; set; }

        public DateTime? finish_date { get; set; }

        public int total_points { get; set; }

        public TrainStatus train_status { get; set; }

        public List<ExamTaskViewModel> attempts { get; set; }

        public static Func<Train, TrainViewModel> ProjectionFunc =
            x => new TrainViewModel
            {
                id = x.Id,
                start_date = x.StartTime,
                finish_date = x.FinishTime,
                total_points = x.TaskAttempts.Sum(y => y.Points),

                train_status = x.TaskAttempts.All(y => !(y is UserManualCheckingTaskAttempt) || ((UserManualCheckingTaskAttempt)y).IsChecked)
                ? TrainStatus.Complete
                : TrainStatus.DontChecked,

                attempts = x.TaskAttempts.Select(y => ExamTaskViewModel.ProjectionFunc(y.ExamTask)).ToList()
            };

    }
}