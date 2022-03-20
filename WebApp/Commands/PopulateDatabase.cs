using MediatR;
using WebApp.Consumer.SeedData;

namespace WebApp.Commands
{
    public class PopulateDatabase: IRequest<Unit>
    {
        
        public PopulateDatabase(SeedData? request)
        {
            Request = request;
        }

        public SeedData? Request { get; }
    }
}