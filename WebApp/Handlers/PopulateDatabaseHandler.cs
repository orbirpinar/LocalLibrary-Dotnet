using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WebApp.Commands;
using WebApp.Seeder;

namespace WebApp.Handlers
{
    public class PopulateDatabaseHandler : IRequestHandler<PopulateDatabase, Unit>
    {
        private readonly ISeederRepository _seederRepository;


        public PopulateDatabaseHandler(ISeederRepository seederRepository)
        {
            _seederRepository = seederRepository;
        }

        public async Task<Unit> Handle(PopulateDatabase request, CancellationToken cancellationToken)
        {
            await _seederRepository.SaveAsync(request.Request);

            return Unit.Value;
        }
    }
}