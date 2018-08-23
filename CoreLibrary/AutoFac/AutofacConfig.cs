using Autofac;
using CoreLibrary.DataContext;
using CoreLibrary.EntityFramework;
using CoreLibrary.Repositories;
using CoreLibrary.UnitOfWork;

namespace CoreLibrary.AutoFac
{
    public static class AutofacConfig
    {
        public static void RegisterLibraryDependencies<T>(this ContainerBuilder builder) where T: EntityFrameworkDataContext<T>
        {
            builder.RegisterType<T>().As<IDataContextAsync>().InstancePerRequest();
            builder.RegisterType<EntityFrameorkUnitOfWork>().As<IUnitOfWorkAsync>().InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepositoryAsync<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>)).InstancePerRequest();
        }
    }
}
