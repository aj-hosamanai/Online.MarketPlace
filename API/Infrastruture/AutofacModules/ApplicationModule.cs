using Application.Online.MarketPlace.Commands;
using Application.Online.MarketPlace.Queries;
using Autofac;
using Infrastruture.Online.MarketPlace.Repository;
using MediatR;
using System.Buffers;
using System.Reflection;

namespace API.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

           builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();

            builder.RegisterType<PartnerDetailRepository>()
            .As<IPartnerDetailRepository>()
            .InstancePerLifetimeScope();
            builder.RegisterType<CatalogueRepository>()
            .As<ICatalogueRepository>()
            .InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>()
           .As<IOrderRepository>()
           .InstancePerLifetimeScope();
            
            builder.RegisterAssemblyTypes(typeof(ListAllPartnerQuery).GetTypeInfo().Assembly).
                                    AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(ListOrderByIdQuery).GetTypeInfo().Assembly).
                                   AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(UpdateCatalogueCommandHandler).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(DeleteCatalogueCommandHandler).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateCatalogueCommandHandler).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreatePartnerCommandHandler).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly).
                                                       AsClosedTypesOf(typeof(IRequestHandler<,>));
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });
        }
    }
}
