using Autofac;
using Customer.Application.Commands.SellerCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Customer.Api.Infraestructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder) 
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(CreateSellerCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
