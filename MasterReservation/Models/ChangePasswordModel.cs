using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class ChangePasswordModel
    {
        [Key]
        public int id { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 50 символов")]
        [Required(ErrorMessage = "Введите новый пароль")]
        public string NewPassword { get; set; }

        [System.Web.Mvc.Compare("NewPassword", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Повторите новый пароль")]
        public string ConfirmPassword { get; set; }
    }
}