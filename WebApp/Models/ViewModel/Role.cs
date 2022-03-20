

using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModel
{
    public class Role
    {

        [Required] public string Name { get; set; } = default!;
    }
}