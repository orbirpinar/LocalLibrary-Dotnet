using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.Commands;
using WebApp.Seeder;

namespace WebApp.Handlers
{
    public class PopulateDatabaseHandler : IRequestHandler<PopulateDatabase, Unit>
    {
        private readonly ISeederRepository _seederRepository;
        private readonly ILogger<PopulateDatabaseHandler> _logger;


        public PopulateDatabaseHandler(ISeederRepository seederRepository, ILogger<PopulateDatabaseHandler> logger)
        {
            _seederRepository = seederRepository;
            _logger = logger;
        }

        public async Task<Unit> Handle(PopulateDatabase request, CancellationToken cancellationToken)
        {
            await _seederRepository.SaveAsync(request.Request);

            return Unit.Value;
        }
    }
}