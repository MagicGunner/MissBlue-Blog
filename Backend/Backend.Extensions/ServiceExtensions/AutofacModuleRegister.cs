using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Backend.Application;
using Backend.Contracts;
using Backend.Domain;
using Backend.Infrastructure;
using Backend.Infrastructure.UnitOfWorks;
using Module = Autofac.Module;

namespace Backend.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Module {
    /*
       1、看是哪个容器起的作用，报错是什么
       2、三步走导入autofac容器
       3、生命周期，hashcode对比，为什么controller里没变化
       4、属性注入
       */
    protected override void Load(ContainerBuilder builder) {
        var basePath = AppContext.BaseDirectory;

        var targetDlls = Directory.GetFiles(basePath, "*.dll", SearchOption.AllDirectories)
                                  .Where(file => file.EndsWith("Application.dll") || file.EndsWith("Domain.dll"))
                                  .ToList();

        var assemblies = targetDlls.Select(Assembly.LoadFrom).ToArray();

        var aopTypes = new List<Type> {
                                          typeof(ServiceAOP),
                                          typeof(TranAOP)
                                      };
        builder.RegisterType<ServiceAOP>();
        builder.RegisterType<TranAOP>();

        builder.RegisterAssemblyTypes(assemblies)
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(BaseRepositories<>))
               .As(typeof(IBaseRepositories<>))
               .InstancePerDependency(); //注册仓储
        builder.RegisterGeneric(typeof(BaseServices<,>))
               .As(typeof(IBaseServices<,>))
               .EnableInterfaceInterceptors()
               .InterceptedBy(aopTypes.ToArray())
               .InstancePerDependency(); //注册服务

        builder.RegisterType<UnitOfWorkManage>()
               .As<IUnitOfWorkManage>()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .PropertiesAutowired();
    }
}