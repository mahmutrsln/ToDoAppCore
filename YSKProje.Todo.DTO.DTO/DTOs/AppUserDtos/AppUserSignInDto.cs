using System;
using System.Collections.Generic;
using System.Text;

namespace YSKProje.ToDo.DTO.DTOs.AppUserDtos
{
    public class AppUserSignInDto
    {
        //[Display(Name = "Kullanıcı Adı : ")]
        //[Required(ErrorMessage = "Boş geçilemez")]
        public string UserName { get; set; }

        //[Display(Name = "Parola : ")]
        //[Required(ErrorMessage = "Boş geçilemez")]
        public string Password { get; set; }

        //[Display(Name = "Beni Hatırla :")]
        public bool RememberMe { get; set; }
    }
}
