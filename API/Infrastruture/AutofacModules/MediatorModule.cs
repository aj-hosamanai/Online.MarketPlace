using Application.Online.MarketPlace.Commands;
using Application.Online.MarketPlace.Queries;
using Autofac;
using MediatR;
using System.Reflection;

namespace API.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(ListOrderByIdQuery).GetTypeInfo().Assembly).
                                  AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(ListAllPartnerQuery).GetTypeInfo().Assembly).
                                    AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreatePartnerCommand).GetTypeInfo().Assembly).
                                             AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateCatalogueCommand).GetTypeInfo().Assembly).
                                           AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteCatalogueCommand).GetTypeInfo().Assembly).
                                        AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UpdateCatalogueCommand).GetTypeInfo().Assembly).
                                        AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommand).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

        }
    }
}
