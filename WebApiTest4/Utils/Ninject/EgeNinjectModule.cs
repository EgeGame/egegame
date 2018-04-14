using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiTest4.Models.ExamsModels;
using WebApiTest4.Services;
using WebApiTest4.Services.Impls;

namespace WebApiTest4.Utils.Ninject
{
    public class EgeNinjectModule: NinjectModule
    {
        public override void Load()
        {
            var context = new ExamAppDbContext();

            this.Bind<ITaskService>().To<TaskServiceImpl>().WithConstructorArgument("context", context);
            this.Bind<IUserService>().To<UserServiceImpl>().WithConstructorArgument("context", context);
            this.Bind<ITopicService>().To<TopicServiceImpl>().WithConstructorArgument("context", context);
            this.Bind<ISchoolService>().To<SchoolServiceImpl>().WithConstructorArgument("context", context);
            this.Bind<ISolvedTasksService>().To<SolvedTasksServiceImpl>().WithConstructorArgument("context", context);
        }
    }
}