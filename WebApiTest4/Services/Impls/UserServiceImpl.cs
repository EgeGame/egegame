using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Util;
using WebGrease.Css.Extensions;

namespace WebApiTest4.Services.Impls
{
    public class UserServiceImpl : IUserService
    {
        private readonly ExamAppDbContext _context;

        public UserServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }

        public UserViewModel GetUser(int id)
        {
            //if this user is teacher return vm without ratings and points
            return
                _context.Users.Where(x => x.Id == id)
                    .OfRole("teacher")
                    .ToList()
                    .Select(x => new UserViewModel(x, null, null, 0))
                    .FirstOrDefault() ??
                GetRatedUserViewModels().FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<UserViewModel> GetRatingForUser(int userId)
        {
            return GetRatedUserViewModels();
        }

        private IEnumerable<UserViewModel> GetRatedUserViewModels()
        {
            var students = _context.Users
                .OfRole("student")
                .Select(
                    x => new
                    {
                        user = x,
                        points = x.Trains.Any()
                            ? ((x.Trains.OfType<ExamTrain>().Any()
                                   ? x.Trains.OfType<ExamTrain>()
                                       .SelectMany(train => train.TaskAttempts)
                                       .Sum(attempt => attempt.Points)
                                   : 0)
                               + (x.Trains.OfType<FreeTrain>().Any()
                                   ? x.Trains.OfType<FreeTrain>()
                                       .SelectMany(ftr => ftr.TaskAttempts)
                                       .GroupBy(ftta => ftta.ExamTask.Id, (z, y) => new {id = z, attempts = y})
                                       .Select(g => g.attempts.Max(t => t.Points))
                                       .Sum()
                                   : 0)
                            )
                            : 0
                    }
                )
                .OrderByDescending(res => res.points)
                .ToList();
            //check need add some badges
            students.Select(x => x.user).AddBadgesIfHaveNot();
            _context.SaveChanges();
            return students.Select(
                    (res, i) =>
                        new UserViewModel(res.user, (i + 1), res.points,
                            Helpers.GetEgePoints(
                                res.user.Trains.OfType<ExamTrain>().Any()
                                    ? res.user.Trains.OfType<ExamTrain>()
                                        .Max(train => train.TaskAttempts?.Sum(attempt => attempt.Points) ?? 0)
                                    : 0)
                        )
                );
        }

        public void AddCurrentExam(User user, Type type)
        {
            //to be sure users exist in the same context
            var dbUser = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            var exam = _context.Exams.AsEnumerable().FirstOrDefault(x => x.GetType() == type);
            dbUser.CurrentExam = _context.Exams.FirstOrDefault(x => x.Id == exam.Id);
            _context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public TeacherViewModel GetTeacherVMById(int id)
        {
            return new TeacherViewModel(GetUserById(id));
        }

        public IEnumerable<TeacherViewModel> GetTeachers()
        {
            return _context
                .Users
                .OfRole("teacher")
                .ToList()
                .Select(x => new TeacherViewModel(x));
        }

        public bool UserExists(int id)
        {
            return GetUserById(id) != null;
        }

        public void ClearDataForTeacher(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                _context.Badges.RemoveRange(user.Badges);
                _context.Trains.RemoveRange(user.Trains);
                _context.SaveChanges();
            }
        }
    }

    public static class BadgesUsersExtension
    {
        private const string ImagesStoragePrefix = "/api/v1/uploads/";
        private const string AllTasksFromTopic = "ALL_TASKS_FROM_TOPIC_";
        private const string GotCountOfTasks = "GOT_COUNT_OF_TASKS_";
        private const string GoodExamPoints = "GOOD_EXAM_POINTS";
        private const int GoodPointsBorder = 56;
        private static readonly int[] Counts = new[] {10, 50, 100, 500};

        public static IEnumerable<User> AddBadgesIfHaveNot(this IEnumerable<User> users)
        {
            users.ForEach(user =>
            {
                if (user.Trains.Any())
                {
                    //#1 badge check in free train solved all tasks
                    user.CurrentExam?.TaskTopics.ForEach(topic =>
                    {
                        //user already has this badge
                        if (!user.Badges.Any(b => b.Description.Equals(AllTasksFromTopic + topic.Id)))
                        {
                            //user has solved all task from this topic
                            if (topic.ExamTasks.Count == (user.Trains.OfType<FreeTrain>().Any()
                                    ? user.Trains.OfType<FreeTrain>()
                                          .SelectMany(
                                              train =>
                                                  train.TaskAttempts.Where(
                                                      attempt =>
                                                          attempt.Points > 0 && attempt.ExamTask.TaskTopic.Id == topic.Id))
                                          ?.GroupBy(attempt => attempt.ExamTask.Id)
                                          .Count() ?? 0
                                    : 0))
                            {
                                user.Badges.Add(new Badge()
                                {
                                    Description = AllTasksFromTopic + topic.Id,
                                    ImageSrc = ImagesStoragePrefix + AllTasksFromTopic + ".png"
                                });
                            }
                        }
                    });

                    //#2 badge check user got enougth task
                    var actualCount = user.Trains.SelectMany(t => t.TaskAttempts).Where(att => att.Points > 0).GroupBy(x => x.ExamTask.Id).Count();
                    foreach (var count in Counts)
                    {
                        if (!user.Badges.Any(b => b.Description.Equals(GotCountOfTasks + count)) && actualCount >= count)
                        {
                            user.Badges.Add(new Badge()
                            {
                                Description = GotCountOfTasks + count,
                                ImageSrc = ImagesStoragePrefix + GotCountOfTasks + ".png"
                            });
                        }
                    }

                    //#3 badge check user got good points
                    if (!user.Badges.Any(b => b.Description.Equals(GoodExamPoints)) &&
                        GoodPointsBorder < (user.Trains.OfType<ExamTrain>().Any()
                            ? user.Trains.OfType<ExamTrain>()
                                .Max(train => train.TaskAttempts?.Sum(attempt => attempt.Points) ?? 0)
                            : 0))
                    {
                        user.Badges.Add(new Badge()
                        {
                            Description = GotCountOfTasks,
                            ImageSrc = ImagesStoragePrefix + GoodExamPoints + ".png"
                        });
                    }
                }
            });
            return users;
        }
    }
}