using System;

namespace WebApp.Models.CreateModel
{
    public class CreateAuthorModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfDeath { get; set; }
    }
}