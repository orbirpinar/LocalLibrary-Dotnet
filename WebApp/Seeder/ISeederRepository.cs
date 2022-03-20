using System.Threading.Tasks;
using WebApp.Consumer.SeedData;

namespace WebApp.Seeder
{
    public interface ISeederRepository
    {
        public Task<SeedData?> SaveAsync(SeedData? seedData);
    }
}