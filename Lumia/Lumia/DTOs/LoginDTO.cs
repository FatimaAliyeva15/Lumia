using System.ComponentModel.DataAnnotations;

namespace Lumia.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; }
        [Required]
        [MinLength(7)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
