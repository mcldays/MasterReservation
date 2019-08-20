using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MasterReservation.Models
{
    public class RegisterMasterModel
    {



        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите Фамилию")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Введите отчество")]
        public string Patronymic { get; set; }
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Введите E-mail")]
        public string Email { get; set; }

        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Некоректный номер телефона")]
        [Required(ErrorMessage = "Введите Номер телефона")]
        public string PhoneNumber { get; set; }


        public string City { get; set; }



        [Required(ErrorMessage = "Выберите услуги")]
        public string Offers { get; set; }


        [Range(0, 50, ErrorMessage = "Недопустимый стаж")]
        public string Expirience { get; set; }
        public string Awards { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password",ErrorMessage = "Пароли не совпадают")]

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Повторите пароль")]
        public string SumbitPassword { get; set; }


    }
}