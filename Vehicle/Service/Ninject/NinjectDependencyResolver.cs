using AutoMapper;
using Ninject;
using Service.Interfaces;
using Service.Models;
using Service.Services;
using System.Web.Mvc;

namespace Service.Ninject
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IVehicleService>().To<VehicleService>();
            kernel.Bind<IVehicleRepository>().To<EFVehicleRepository>();
        }
    }
}
