using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserAddDto
    {
        // [Display(Name = "Kullanıcı Adı : ")]
        //  [Required(ErrorMessage = "Kullanıcı adı boş geçilemez")]
        public string UserName { get; set; }

        // [Display(Name = "Parola : ")]
        // [Required(ErrorMessage = "Parola boş geçilemez")]
        // [DataType(DataType.Password)]
        public string Password { get; set; }

        // [Display(Name = "Parola Tekrar : ")]
        // [Compare("Password", ErrorMessage = "Parolalar Eşleşmiyor")]
        // [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //[EmailAddress(ErrorMessage = "Geçersiz email")]
        //[Display(Name = "Email : ")]
        //[Required(ErrorMessage = "Email boş geçilemez")]
        public string Email { get; set; }

        //[Display(Name = "Ad : ")]
        //[Required(ErrorMessage = "Ad boş geçilemez")]
        public string Name { get; set; }

        //[Display(Name = "Soyad : ")]
        //[Required(ErrorMessage = "Soyad boş geçilemez")]
        public string SurName { get; set; }

    }
}
