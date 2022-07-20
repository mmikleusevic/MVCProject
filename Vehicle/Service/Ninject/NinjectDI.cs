using Ninject.Modules;
using Service.Services.VehicleMake;
using Service.Services.VehicleModel;

namespace Service.Ninject
{
    public class NinjectDI : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeService>().To<VehicleMakeService>();
            Bind<IVehicleModelService>().To<VehicleModelService>();
        }
    }
}
