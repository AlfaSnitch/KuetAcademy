using System.ComponentModel.DataAnnotations;

namespace KuetAcademy.Web.Models.ViewModels
{
    public class AddTagRequest
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string displayName { get; set; }
    }
}
