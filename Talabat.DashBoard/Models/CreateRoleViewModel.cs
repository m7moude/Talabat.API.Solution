using System.ComponentModel.DataAnnotations;

namespace Talabat.DashBoard.Models
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
    }
}
