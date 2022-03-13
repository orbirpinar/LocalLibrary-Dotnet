using System.Collections.Generic;
using MediatR;
using WebApp.Dto;

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