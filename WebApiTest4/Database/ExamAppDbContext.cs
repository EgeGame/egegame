using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using WebApiTest4.Migrations;
using WebApiTest4.Parsing;

namespace WebApiTest4.Models.ExamsModels
{
    public class ExamAppDbContext : IdentityDbContext<User, RoleIntPk, int, UserLoginIntPk, UserRoleIntPk, UserClaimIntPk>
    {
        static ExamAppDbContext()
        {
           Database.SetInitializer(new EgeDbIntializer());
        }

        public ExamAppDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<TaskTopic> TaskTopics { get; set; }
        public virtual DbSet<ExamTask> Tasks { get; set; }
        public virtual DbSet<Train> Trains { get; set; }
        public virtual DbSet<ExamTrain> ExamTrains { get; set; }
        public virtual DbSet<FreeTrain> FreeTrains { get; set; }
        public virtual DbSet<UserTaskAttempt> UserTaskAttempts { get; set; }
        public virtual DbSet<Badge> Badges { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        public static ExamAppDbContext Create()
        {
            return new ExamAppDbContext();
        }
    }

    class EgeDbIntializer : MigrateDatabaseToLatestVersion<ExamAppDbContext, Configuration>
    {
        public override void InitializeDatabase(ExamAppDbContext db)
        {

            db.Database.CreateIfNotExists();
            InitExams(db);

            Scanner scanner = new Scanner();
            var synchTask = scanner.AddNewTasks(db);
            synchTask.Wait();

            InitRoles(db);
        }

        private void InitExams(ExamAppDbContext context)
        {
            if(!context.Exams.OfType<EgeExam>().Any())
            {
                context.Exams.Add(new EgeExam());
            }
            if (!context.Exams.OfType<OgeExam>().Any())
            {
                context.Exams.Add(new OgeExam());
            }

            context.SaveChanges();
        }

        private void InitRoles(ExamAppDbContext context)
        {
            if (!context.Roles.Any(x => x.Name == "student"))
            {
                context.Roles.Add(new RoleIntPk
                {
                    Name = "student"
                });
            }

            if (!context.Roles.Any(x => x.Name == "teacher"))
            {
                context.Roles.Add(new RoleIntPk
                {
                    Name = "teacher"
                });
            }

            context.SaveChanges();
        }
    }
}