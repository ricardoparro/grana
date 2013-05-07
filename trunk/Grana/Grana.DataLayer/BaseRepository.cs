using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Integration.Mvc;
using Grana.DataLayer.Interfaces;
using Grana.DataLayer.Repository;
using Grana.Model;

namespace Grana.DataLayer
{
    public class BaseRepository : Module, IBaseRepository
    {
        public GranaDataDataContext context;

        public GranaDataDataContext Context { get { return context; } set { context = value; } }

        //override Load method
        protected override void Load(ContainerBuilder builder)
        {
            //say that for any IUsersRepository we need UsersRepository class to be invoked
            builder.Register(c => new ApplicationRepository()).As<IApplicationRepository>().InstancePerHttpRequest();
        }
    }
}
