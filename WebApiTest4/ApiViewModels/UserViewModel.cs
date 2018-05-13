using System.Collections.Generic;
using System.Linq;
using WebApiTest4.Models.ExamsModels;

namespace WebApiTest4.ApiViewModels
{
    public class UserViewModel
    {
        public UserViewModel(User sourceUser, int? ratingPlace, int? points, int usePoint)
        {
            id = sourceUser.Id;
            name = sourceUser.Name;
            avatar = sourceUser.Avatar;
            this.points = points;
            rating_place = ratingPlace;
            use_point = sourceUser.UsePoints;

            if (sourceUser.Teacher != null)
            {
                teacher_id = sourceUser.Teacher.Id;
                teacher_name = sourceUser.Teacher.Name;
            }

            if (sourceUser.School != null)
            {
                school_id = sourceUser.School.Id;
                school_number = sourceUser.School.Title;
            }

            exam_type = sourceUser.CurrentExam is EgeExam ? 0 : 1;
            use_point = usePoint;
            badges = sourceUser.Badges.Select(x => new BadgeViewModel(x)).ToList();
        }

        public int id { get; private set; }
        public string name { get; private set; }
        public string avatar { get; private set; }

        public string school_number { get; set; }
        public int? school_id { get; set; }

        public string teacher_name { get; set; }
        public int? teacher_id { get; set; }

        public int exam_type { get; set; }
        public int? points { get; private set; }
        public int? rating_place { get; private set; }
        public int use_point { get; private set; }
        public List<BadgeViewModel> badges { get; set; }
    }
}