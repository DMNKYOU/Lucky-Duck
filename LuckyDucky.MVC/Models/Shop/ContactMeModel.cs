using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace LuckyDucky.MVC.Models
{
    public class ContactMeModel
    {
        [Required(ErrorMessage = "NameErrorReq")]
        //^[A-Za-z]*[[A-Za-z]*|[ ]?]*[A-Za-z]+$ spaces in name?
        //[RegularExpression("^[A-Za-z]+$", ErrorMessage = "NameErrorRegEx")]
        [RegularExpression("([A-Za-z]*([A-Za-z]*|[ ]?)*[A-Za-z]+)|([A-Яа-я]*([А-Яа-я]*|[ ]?)*[А-Яа-я]+)", ErrorMessage = "NameErrorRegEx")]
        [Display(Name = "Name", Prompt = "NamePromptMessage")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EmailErrorReq")]
        [EmailAddress(ErrorMessage = "EmailErrorRegEx")]
        [Display(Name = "Email", Prompt = "EmailPromptMessage")]
        public string Email { get; set; }

        [Required(ErrorMessage = "MessageErrorReq")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "MessageErrorLen")]
        [Display(Name = "Message", Prompt = "PromptMessage")]
        public string Message { get; set; }

        public ContactMeInfo ContactMeInfo { get; set; }
    }
}
