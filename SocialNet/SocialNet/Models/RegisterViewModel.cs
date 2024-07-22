using System.ComponentModel.DataAnnotations;

namespace SocialNet.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя", Prompt ="Введите имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email", Prompt = "Введите email")]
        public string EmailReg { get; set; }

        [Required]
        [Display(Name = "Год", Prompt = "Выберите год")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "День", Prompt = "Выберите день")]
        public int Date { get; set; }

        [Required]
        [Display(Name = "Месяц", Prompt = "Выберите месяц")]
        public int Month { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль", Prompt = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string PasswordReg { get; set; }

        [Required]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль", Prompt = "Подтвердите пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Никнейм")]
        public string Login => EmailReg;
        public string MiddleName => LastName;
    }
}
