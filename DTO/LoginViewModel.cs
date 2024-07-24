using System.ComponentModel.DataAnnotations;

namespace TestAbsensi.DTO
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
