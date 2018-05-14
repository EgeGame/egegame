using System;
using System.Linq.Expressions;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class ExamTaskViewModel
    {
        public ExamTaskViewModel() { }

        public ExamTaskViewModel(ExamTask sourseTask, int? user_points = null)
        {
            id = sourseTask.Id;
            text = sourseTask.Text;
            topic_id = sourseTask.TaskTopic.Id;
            type = sourseTask.TaskTopic.IsShort ? 0 : 1;
            code = sourseTask.TaskTopic.Code;
            max_points = sourseTask.TaskTopic.PointsPerTask;
            this.user_points = user_points;
        }

        public static Expression<Func<ExamTask, ExamTaskViewModel>> ProjectionExpression =
            x => new ExamTaskViewModel
            {
                id = x.Id,
                text = x.Text,

                topic_id = x.TaskTopic.Id,
                code = x.TaskTopic.Code,
                type = x.TaskTopic.IsShort ? 0 : 1,
                max_points = x.TaskTopic.PointsPerTask
            };

        public int topic_id { get; set; }
        public int type { get; set; }
        public int id { get; set; }
        public string text { get; set; }
        public string code { get; set; }
        public int max_points { get; set; }
        public int? user_points { get; set; }


    }
}