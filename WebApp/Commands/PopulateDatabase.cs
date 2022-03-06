using System.Collections.Generic;
using MediatR;
using WebApp.Dto;

namespace WebApp.Commands
{
    public class PopulateDatabase: IRequest<Unit>
    {
        
        public PopulateDatabase(List<SeedData>? request)
        {
            Request = request;
        }

        public List<SeedData>? Request { get; }
    }
}