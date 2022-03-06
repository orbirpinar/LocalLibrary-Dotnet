using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using NuGet.Common;
using WebApp.Commands;
using WebApp.Seeder;

namespace WebApp.Handlers
{
    public class PopulateDatabaseHandler: IRequestHandler<PopulateDatabase,Unit>
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
            _logger.LogInformation("Mediator work!!");
            await _seederRepository.SaveAll(request.Request);
            return Unit.Value;
        }
    }
}