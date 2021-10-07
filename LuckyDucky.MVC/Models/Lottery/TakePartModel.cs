using System.ComponentModel.DataAnnotations;

namespace LuckyDucky.MVC.Models
{
    public class TakePartModel
    {
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "This field can be contain only letters")]
        [Display(Name = "Name", Prompt = "Name")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Incorrect Email")]
        [Display(Name = "Email", Prompt = "example@example.org")]
        public string Email { get; set; }

        [Display(Name = "Always take part")]
        public bool IsAlwaysTakePartCheck { get; set; } = false;
    }
}