using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Dto;

namespace WebApp.Seeder
{
    public interface ISeederRepository
    {
        public Task SaveAll(List<SeedData>? seedDataList);
    }
}