using System.ComponentModel.DataAnnotations;

namespace KuetAcademy.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }   
    }
}
