namespace Service.Models
{
    public class Search
    {
        public static Task<IQueryable<VehicleMake>> SearchVehicleMake(IQueryable<VehicleMake> model, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            return Task.FromResult(model);
        }

        public static Task<IQueryable<VehicleModel>> SearchVehicleModel(IQueryable<VehicleModel> model, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(s => s.Name.Contains(searchString)
                                       || s.Abrv.Contains(searchString));
            }
            return Task.FromResult(model);
        }
    }
}
