namespace Service.Models
{
    public class Sort
    {
        public static Task<IQueryable<VehicleMake>> SortByMake(IQueryable<VehicleMake> model,string sortOrder)
        {
            switch (sortOrder)
            {
                case "id":
                    model = model.OrderBy(x => x.Id);
                    break;
                case "id_desc":
                    model = model.OrderByDescending(x => x.Id);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "abrv":
                    model = model.OrderBy(s => s.Abrv);
                    break;
                case "abrv_desc":
                    model = model.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    model = model.OrderBy(s => s.Name);
                    break;
            }
            return Task.FromResult(model);
        }

        public static Task<IQueryable<VehicleModel>> SortByModel(IQueryable<VehicleModel> model, string sortOrder)
        {
            switch (sortOrder)
            {
                case "id":
                    model = model.OrderBy(x => x.Id);
                    break;
                case "id_desc":
                    model = model.OrderByDescending(x => x.Id);
                    break;
                case "name_desc":
                    model = model.OrderByDescending(s => s.Name);
                    break;
                case "abrv":
                    model = model.OrderBy(s => s.Abrv);
                    break;
                case "abrv_desc":
                    model = model.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    model = model.OrderBy(s => s.Name);
                    break;
            }


            return Task.FromResult(model);
        }
    }
}
