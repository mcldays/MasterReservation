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
        public RegisterMasterModel(ResidentModel res = null)
        {
            if (res != null)
            {
                Name = res.Name;
                Surname = res.Surname;
                Patronymic = res.Patronymic;
                Email = res.Email;
                PhoneNumber = res.Phone;
                City = res.City;
                Offers = res.Offers;
                Expirience = res.Experience;
                Awards = res.Awards;
                Password = res.Password;
                id = res.Id;

            }
        }

        public RegisterMasterModel()
        {

        }
      
        public int id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Фамилию")]
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [RegularExpression(@"^\+\d{1,3}\s?\(\d{3}\)\s?\d{3}(\d{2}){2}$", ErrorMessage = "Некоректный номер телефона")]
        [Required(ErrorMessage = "Введите Номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Выберите предоставляемые услуги услуги")]
        public string Offers { get; set; }

        [Range(0, 50, ErrorMessage = "Недопустимый стаж")]
        public string Expirience { get; set; }

        public string Awards { get; set; }


       
        
       
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 50 символов")]
        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [System.Web.Mvc.Compare("Password",ErrorMessage = "Пароли не совпадают")]

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Повторите пароль")]
        public string SumbitPassword { get; set; }
    }
}