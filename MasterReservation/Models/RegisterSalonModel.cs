using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MasterReservation.Models
{
    public class RegisterSalonModel
    {
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [RegularExpression(@"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}(\d{2}){2}$", ErrorMessage = "Некоректный номер телефона")]
        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string City { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Required(ErrorMessage = "Введите электронную почту")]
        public string Email { get; set; }


        public string Information { get; set; }



    }
}