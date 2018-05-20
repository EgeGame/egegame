using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.ApiViewModels.BindingModels;
using WebApiTest4.Extensions;
using WebApiTest4.Models.ExamsModels;
using WebGrease.Css.Extensions;

namespace WebApiTest4.Services.Impls
{
    public class SolvedTasksServiceImpl : ISolvedTasksService
    {
        private readonly ExamAppDbContext _context;

        public SolvedTasksServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AttemptViewModel> GetUncheckedAttemptsOfMyStudents(int teacher_id, bool? is_checked, int offset, int limit)
        {
            var teacher = _context.Users.OfRole("teacher").FirstOrDefault(x => x.Id == teacher_id);
            if (teacher != null)
            {
                return
                    teacher.Students.SelectMany(
                            student =>
                                student.Trains.SelectMany(
                                    train =>
                                        train.TaskAttempts.OfType<UserManualCheckingTaskAttempt>()
                                            .Where(attempt => attempt.UserAnswer != null)
                                            .WhereIf(is_checked.HasValue, x => x.IsChecked == is_checked)))
                        .OrderByDescending(attempt => attempt.Train.FinishTime)
                        .Skip(offset)
                        .Take(limit)
                        .ToList()
                        .Select(attempt => new AttemptViewModel(attempt));
            }
            return null;
        }

        public IEnumerable<AttemptViewModel> GetUncheckedAttemptsByTopic(int topic_id, bool? is_checked, int teacher_id, int offset, int limit)
        {
            var teacher = _context.Users.OfRole("teacher").FirstOrDefault(x => x.Id == teacher_id);
            if (teacher != null)
            {
                return
                    teacher.Students.SelectMany(
                            student =>
                                student.Trains.SelectMany(
                                    train =>
                                        train.TaskAttempts.OfType<UserManualCheckingTaskAttempt>()
                                            .Where(x => x.ExamTask.TaskTopic.Id == topic_id)
                                            .Where(attempt => attempt.UserAnswer != null)
                                            .WhereIf(is_checked.HasValue, x => x.IsChecked == is_checked)))
                        .OrderByDescending(attempt => attempt.Train.FinishTime)
                        .Skip(offset)
                        .Take(limit)
                        .ToList()
                        .Select(attempt => new AttemptViewModel(attempt));
            }
            return null;
        }

        public IEnumerable<AttemptViewModel> GetUncheckedAttemptsByType(int type, bool? is_checked, int teacher_id, int offset, int limit)
        {
            var isShort = type == 0;

            var teacher = _context.Users.OfRole("teacher").FirstOrDefault(x => x.Id == teacher_id);
            if (teacher != null)
            {
                return
                    teacher.Students.SelectMany(
                            student =>
                                student.Trains.SelectMany(
                                    train =>
                                        train.TaskAttempts.OfType<UserManualCheckingTaskAttempt>()
                                            .Where(x => x.ExamTask.TaskTopic.IsShort == isShort)
                                            .Where(attempt => attempt.UserAnswer != null)
                                            .WhereIf(is_checked.HasValue, x => x.IsChecked == is_checked)))
                        .OrderByDescending(attempt => attempt.Train.FinishTime)
                        .Skip(offset)
                        .Take(limit)
                        .ToList()
                        .Select(attempt => new AttemptViewModel(attempt));
            }
            return null;
        }

        public IEnumerable<AttemptViewModel> GetUncheckedAttemptsByStudent(int student_id, bool? is_checked, int teacher_id, int offset, int limit)
        {
            var teacher = _context.Users.OfRole("teacher").FirstOrDefault(x => x.Id == teacher_id);
            if (teacher != null)
            {
                return
                    teacher.Students
                    .Where(x => x.Id == student_id)
                    .SelectMany(
                            student =>
                                student.Trains.SelectMany(
                                    train =>
                                        train.TaskAttempts.OfType<UserManualCheckingTaskAttempt>()
                                            .Where(attempt => attempt.UserAnswer != null)
                                            .WhereIf(is_checked.HasValue, x => x.IsChecked == is_checked)))
                        .OrderByDescending(attempt => attempt.Train.FinishTime)
                        .Skip(offset)
                        .Take(limit)
                        .ToList()
                        .Select(attempt => new AttemptViewModel(attempt));
            }
            return null;
        }


        public void CheckAttemptsByTeacher(int teacherId, IEnumerable<CheckedAttemptBindigModel> checkedAttempts)
        {
            User teacher = _context.Users.FirstOrDefault(x => x.Id == teacherId);
            if (teacher != null)
            {
                //_context
                //    .UserTaskAttempts
                //    .OfType<UserManualCheckingTaskAttempt>()
                //    .Where(x => !x.IsChecked)
                //    .ToList()
                //    .Join(checkedAttempts, uncheckedAttemptEntity => uncheckedAttemptEntity.Id,
                //        checkedAttemptModel => checkedAttemptModel.attempt_id,
                //        (entity, model) => new {entity = entity, model = model})
                //    .ForEach(
                //        x =>
                //        {
                //            int gotPoints = x.model.points;
                //            int maxPoints = x.entity.ExamTask.TaskTopic.PointsPerTask;
                //            x.entity.Points = (gotPoints < 0 ? 0 : (gotPoints > maxPoints ? maxPoints : gotPoints));
                //            x.entity.CheckTime = DateTime.Now;
                //            x.entity.IsChecked = true;
                //            x.entity.Reviewer = teacher;
                //            x.entity.Train = x.entity.Train;
                //        });
            }
            _context.SaveChanges();
        }
    }
}