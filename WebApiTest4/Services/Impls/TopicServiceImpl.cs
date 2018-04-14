using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.ApiViewModels;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.Services.Impls
{
    public class TopicServiceImpl : ITopicService
    {
        private readonly ExamAppDbContext _context;

        public TopicServiceImpl(ExamAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ExamTopicViewModel> GetTopics()
        {
            return _context.TaskTopics.OrderBy(x => x.Id).ToList().Select(x => new ExamTopicViewModel(x, 0));
        }

        public IEnumerable<ExamTopicViewModel> GetTopicsForUser(int userId)
        {
            var student = _context.Users.OfRole("student").FirstOrDefault(x => x.Id == userId);
            if (student != null)
            {
                return
                    _context.TaskTopics.OrderBy(x => x.Id)
                        .ToList()
                        .Select(
                            topic =>
                                new ExamTopicViewModel(topic,
                                    student.Trains.OfType<FreeTrain>().Any() ?
                                    student.Trains
                                        .OfType<FreeTrain>()
                                        .SelectMany(
                                            train =>
                                                train.TaskAttempts.Where(
                                                    attempt => attempt.Points > 0 && attempt.ExamTask.TaskTopic.Id == topic.Id))
                                        ?.GroupBy(attempt => attempt.ExamTask.Id)
                                        .Count() ?? 0 : 0));
            }
            return GetTopics();
        }
    }
}