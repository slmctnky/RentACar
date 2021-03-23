using Castle.DynamicProxy;
using Core.Aspects.Autofac.Exception;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
           
            try
            {
                var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

                var methodAttributes = type.GetMethod(method.Name, method.GetParameters().Select(p => p.ParameterType).ToArray()).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

                //var methodAttributes = type.GetMethod(method.Name);
                //var methodAttributes2 = methodAttributes.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                classAttributes.AddRange(methodAttributes);
                classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));

                return classAttributes.OrderBy(x => x.Priority).ToArray();
            }
            catch (Exception ex)
            {

                throw;
            }
          
            
        }
    }
}
