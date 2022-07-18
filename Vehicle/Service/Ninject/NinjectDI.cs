using Ninject.Modules;
using Service.Interfaces;
using Service.Models;
using Service.Services;

namespace Service.Ninject
{
    public class NinjectDI : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>();
            Bind<IVehicleRepository>().To<EFVehicleRepository>();
        }
    }
}
