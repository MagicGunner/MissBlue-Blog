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
        List<string> dllList = [
                                   "Backend.Modules.Blog.Application.dll",
                                   "Backend.Modules.Blog.Domain.dll",
                               ];
        foreach (var se in dllList) {
            LoadByDllName(builder, Path.Combine(basePath, se));
        }
    }

    private void LoadByDllName(ContainerBuilder builder, string dllName) {
        var assembly = Assembly.LoadFrom(dllName);
        builder.RegisterAssemblyTypes(assembly)
               .AsImplementedInterfaces()
               .PropertiesAutowired()
               .InstancePerDependency();
    }
}