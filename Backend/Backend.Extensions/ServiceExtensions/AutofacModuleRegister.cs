using System.Reflection;
using Autofac;
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

        builder.RegisterAssemblyTypes(assemblies)
               .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}