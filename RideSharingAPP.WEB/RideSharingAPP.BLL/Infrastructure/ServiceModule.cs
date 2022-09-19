using Ninject.Modules;
using RideSharingApp.DAL.Interfaces;
using RideSharingApp.DAL.Repositories;
using RideSharingApp.DAL.Entities;

namespace RideSharingApp.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);   
        }
    }
}
